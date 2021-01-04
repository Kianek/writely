# Stage 1 - Set up base .NET dependencies
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything over to the image
COPY . ./

# Publish everything
RUN dotnet publish -c Release -o /app/out

# Stage 2 - Set up ASP.NET Core 
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

# Copy everything from build
COPY --from=build /app/out .

EXPOSE 5000
ENTRYPOINT ["dotnet", "Writely.dll"]