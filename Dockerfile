FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

# Copy everything else and build
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENTRYPOINT ["dotnet", "DotNetProject.Api.dll"]
EXPOSE 80/tcp 