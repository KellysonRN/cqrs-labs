FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./src
WORKDIR /app/src/DotNetProject.Api
RUN dotnet publish -c Release -o out --configfile docker.nuget.config

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app/src/DotNetProject.Api
COPY --from=build-env /app/src/DotNetProject.Api/out/ .
ENTRYPOINT ["dotnet", "DotNetProject.Api.dll"]