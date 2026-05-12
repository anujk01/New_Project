FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Ecommerce/Ecommerce.sln", "Ecommerce/"]
COPY ["Ecommerce/Ecommerce/Ecommerce.csproj", "Ecommerce/Ecommerce/"]

# Restore dependencies
RUN dotnet restore "Ecommerce/Ecommerce.sln"

# Copy everything else
COPY . .

# Build the project
WORKDIR "/src/Ecommerce/Ecommerce"
RUN dotnet build "Ecommerce.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Ecommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render uses the PORT environment variable. 
ENV PORT=10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "Ecommerce.dll"]
