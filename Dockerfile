FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY SandAndStonesEngineSample/SandAndStonesEngineSample.csproj SandAndStonesEngineSample/
COPY SandAndStonesEngine/SandAndStonesEngine.csproj SandAndStonesEngine/
COPY SandAndStonesEngineTests/SandAndStonesEngineTests.csproj SandAndStonesEngineTests/
RUN dotnet restore SandAndStonesEngine/SandAndStonesEngine.csproj
RUN dotnet restore SandAndStonesEngineSample/SandAndStonesEngineSample.csproj
COPY . .

WORKDIR /src/SandAndStonesEngineSample
RUN dotnet build SandAndStonesEngineSample.csproj -c Release -o /app

WORKDIR /src/SandAndStonesEngineSample
RUN dotnet build SandAndStonesEngineSample.csproj -c Release -o /app

FROM build AS testrunner
WORKDIR /src/SandAndStonesEngineTests
CMD ["dotnet", "test", "--no-restore"]

FROM build AS publish
WORKDIR /src/SandAndStonesEngineSample
FROM build AS publish
RUN dotnet publish SandAndStonesEngineSample.csproj -c Release -o /app
