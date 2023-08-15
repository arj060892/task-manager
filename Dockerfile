# Build Stage
FROM mcr.microsoft.com/dotnet/sdk AS build-env
WORKDIR /app

# Copy and restore the API project
COPY TaskManager.API/*.csproj ./TaskManager.API/
RUN dotnet restore ./TaskManager.API/

# Copy and restore the class library projects
COPY TaskManager.CommandQuery/*.csproj ./TaskManager.CommandQuery/
COPY TaskManager.Data/*.csproj ./TaskManager.Data/
COPY TaskManager.IoC/*.csproj ./TaskManager.IoC/
RUN dotnet restore ./TaskManager.CommandQuery/
RUN dotnet restore ./TaskManager.Data/
RUN dotnet restore ./TaskManager.IoC/

# Copy the entire solution
COPY . ./

# Publish the API project
WORKDIR /app/TaskManager.API
RUN dotnet publish -c Debug -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/TaskManager.API/out .
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
