# Starting from MS's dotnet image that has all the SDKs installed,
# build and unit test the app
FROM mcr.microsoft.com/dotnet/sdk:6.0.100-bullseye-slim AS build

COPY . /
WORKDIR /
RUN dotnet restore DotNetProject.sln

# Build
RUN dotnet build --configuration Release --no-restore DotNetProject.sln

# Create dotnet artifacts
RUN dotnet publish --no-restore -c Release --output /app DotNetProject.sln

# Build the deployment container. Switch base images from 'sdk' to
# 'runtime', and use Apline Linux, to reduce image size
FROM mcr.microsoft.com/dotnet/sdk:6.0.100-alpine3.14 AS runtime

# Set up the app to run
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "DotNetProject.Api.dll"]