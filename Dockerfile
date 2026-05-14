FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["project001backend/project001backend.sln", "project001backend/"]
COPY ["project001backend/project001backend/project001backend.csproj", "project001backend/project001backend/"]

# Restore dependencies
RUN dotnet restore "project001backend/project001backend.sln"

# Copy everything else
COPY . .

# Build the project
WORKDIR "/src/project001backend/project001backend"
RUN dotnet build "project001backend.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "project001backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render uses the PORT environment variable. 
ENV PORT=10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "project001backend.dll"]
