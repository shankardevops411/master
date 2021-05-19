FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Scheduleservice.API/Scheduleservice.API.csproj"  --configfile ./NuGet/NuGet.Config  
WORKDIR "/src/Scheduleservice.API"
RUN dotnet build "Scheduleservice.API.csproj" -c Release -o /app
RUN dotnet publish "Scheduleservice.API.csproj" -c Release -o /app
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN sed -i '/^ssl_conf = ssl_sect$/s/^/#/' /etc/ssl/openssl.cnf
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Scheduleservice.API.dll"]
