FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy everything
COPY src/ ./

# Setup Website
RUN cd /app/Website && \
    dotnet restore && \
    dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/Website/out ./
ENTRYPOINT ["dotnet", "Website.dll"]