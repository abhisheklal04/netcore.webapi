FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WebApi/*.csproj WebApi/

RUN dotnet restore

# copy everything else and build app
COPY . .
RUN dotnet publish -c Release -o /publish *.sln

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
