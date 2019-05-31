using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Common;

namespace Repository
{
    public partial class CustomDbContext : CustomerContext
    {
        public static bool EnableEFLogger = false;

        private readonly IOptions<AppSettings> _appSettings;

        public CustomDbContext(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public string ConnectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CustomDbContext.EnableEFLogger = _appSettings.Value.EnableEFLogger;

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
                optionsBuilder.EnableSensitiveDataLogging(); // Show detailed error in LINQ breakpoints.
            }
            if (CustomDbContext.EnableEFLogger)
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
