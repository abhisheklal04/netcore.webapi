using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AutoMapper;
using WebApi.Repository;
using WebApi.Common;
using WebApi.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace WebApi
{
    public class Startup
    {
        public static bool EnableEFLogger = false;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;            
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Startup.EnableEFLogger = Configuration.GetValue<bool>("App:EnableEFLogger");

            services.AddMemoryCache();

            services.AddMvcCore(o =>
            {
                if (Configuration.GetValue<bool>("App:EnableCurlLogger"))
                    o.Filters.Add(new CurlLoggerAttribute());
            })
            .AddApiExplorer()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()); // Responses to show enums as strings rather than ints.
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .AddJsonFormatters(settings =>
            {
                settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                settings.Formatting = Formatting.Indented;
                //settings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();
            services.AddDbContext<CustomDbContext>();
            services.AddWebEncoders();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

            string authorityUrl = Configuration.GetValue<string>("Web:AppServer");
            services.AddSingleton(provider => Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Title = "My API",
                        Version = "v1",
                        Description = @"You must authenticate with login endpoint before calling these endpoints.
This can be done with a POST to " + @"/login with your app client ID.

401 - Not authenticated or role does not have permission to perform this action.
404 - Item by ID is not found or endpoint URL is wrong.
400 - Validation error. Example JSON response body: { ""type"": ""TitleTooLongException"", message: ""Title must be less than 50 characters."" }
500 - Unknown server error.
",
                    });
                //c.CustomSchemaIds(x => x.FullName);
                //c.OrderActionsBy(r => r.RelativePath);

            });

            services.AddAutoMapper(typeof(Startup));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddTransient<CustomerService, CustomerService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.EnableDeepLinking();
            });

            if (Configuration.GetValue<bool>("Debug:SeedData"))
                SeedDatabase.Run(serviceProvider, Configuration);
        }
    }

    public class CurlLoggerAttribute : TypeFilterAttribute
    {
        public CurlLoggerAttribute() : base(typeof(AutoLogActionFilterImpl)) { }
        private class AutoLogActionFilterImpl : IActionFilter
        {
            private readonly ILogger _logger;

            public AutoLogActionFilterImpl(ILogger<CurlLoggerAttribute> logger)
            {
                _logger = logger;
            }
            public void OnActionExecuted(ActionExecutedContext context)
            {
                
            }
            public void OnActionExecuting(ActionExecutingContext context)
            {
                var request = context.HttpContext.Request;
                StringBuilder curl = new StringBuilder("curl " + request.Scheme + "://" + request.Host + request.Path);
                curl.Append(" -X " + request.Method);
                var authorizationHeader = request.Headers["Authorization"];
                if (authorizationHeader.Any())
                    curl.Append(" -H 'Authorization: " + authorizationHeader[0] + "'");

                if (request.Body.CanSeek)
                {
                    request.Body.Position = 0;
                    using (StreamReader reader = new StreamReader(request.Body))
                        curl.Append(" --data '" + reader.ReadToEnd() + "'");
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(curl.ToString());
                Console.ResetColor();

                _logger.LogInformation("CurlRequest : " + curl.ToString());

            }

        }
    }

    public class ErrorHandlingMiddleware
    {
        private static ILogger _logger;
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context, IHostingEnvironment env)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                if (!env.IsDevelopment()) { }                    
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is Exception) code = HttpStatusCode.BadRequest;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine((exception.Source == nameof(webapi) ? "App Error - " : "Unhandled Error - ") + exception.GetType().Name + ": " + exception.Message);
            _logger.LogError((exception.Source == nameof(webapi) ? "App Error - " : "Unhandled Error - ") + exception.GetType().Name + ": " + exception.Message);
            Console.ResetColor();
            if (exception.Source != nameof(webapi))
            {
                // Exception was not one of ours.
                // Do additional logging here.
            }
            var result = JsonConvert.SerializeObject(new { type = exception.GetType().Name, message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public partial class CustomDbContext : CustomerContext
    {
        public static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();
        public string ConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("InMemoryDb", InMemoryDatabaseRoot);
                optionsBuilder.EnableSensitiveDataLogging(); // Show detailed error in LINQ breakpoints.
            }
            if (Startup.EnableEFLogger)
            {
                var lf = new LoggerFactory();
                lf.AddProvider(new EFLoggerProvider());
                optionsBuilder.UseLoggerFactory(lf);
            }
            else
                optionsBuilder.UseLoggerFactory(new LoggerFactory());
        }

        public class EFLoggerProvider : ILoggerProvider
        {
            public ILogger CreateLogger(string categoryName) { return new MyLogger(); }
            public void Dispose() { }
            private class MyLogger : ILogger
            {
                public bool IsEnabled(LogLevel logLevel) { return true; }
                private static ConsoleColor _lastColor;
                public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
                {
                    if (eventId.Id == 20101) // "Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted"
                    {
                        _lastColor = _lastColor == ConsoleColor.Yellow ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
                        Console.ForegroundColor = _lastColor;
                        Console.WriteLine(formatter(state, exception));
                        Console.ResetColor();
                        Debug.WriteLine(formatter(state, exception));
                    }
                }
                public IDisposable BeginScope<TState>(TState state) { return null; }
            }
        }

        public CustomDbContext(string connectionString) : base()
        {
            ConnectionString = connectionString;
        }

        public CustomDbContext(DbContextOptions options) : base(options) { }
    }

}
