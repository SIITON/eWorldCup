# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY src/*.sln ./
COPY src/eWorldCup.API/*.csproj ./eWorldCup.API/
COPY src/eWorldCup.Core/*.csproj ./eWorldCup.Core/
COPY src/eWorldCup.Application/*.csproj ./eWorldCup.Application/
COPY src/eWorldCup.Infrastructure/*.csproj ./eWorldCup.Infrastructure/
COPY src/eWorldCup.Application.Tests/*.csproj ./eWorldCup.Application.Tests/
COPY src/eWorldCup.Core.Tests/*.csproj ./eWorldCup.Core.Tests/
COPY src/eWorldCup.Console/*.csproj ./eWorldCup.Console/
# Add other project references as needed

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/eWorldCup.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "eWorldCup.API.dll"]
