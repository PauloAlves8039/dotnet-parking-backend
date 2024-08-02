FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Parking.WebAPI/Parking.WebAPI.csproj", "src/Parking.WebAPI/"]
COPY ["src/Parking.IoC/Parking.IoC.csproj", "src/Parking.IoC/"]
COPY ["src/Parking.Data/Parking.Data.csproj", "src/Parking.Data/"]
COPY ["src/Parking.Model/Parking.Model.csproj", "src/Parking.Model/"]
COPY ["src/Parking.Service/Parking.Service.csproj", "src/Parking.Service/"]
RUN dotnet restore "./src/Parking.WebAPI/Parking.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Parking.WebAPI"
RUN dotnet build "./Parking.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Parking.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parking.WebAPI.dll"]