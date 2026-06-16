# =========================================================
# Etapa 1: Entorno de ejecución (Runtime)
# =========================================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
# Render asigna dinámicamente un puerto a través de la variable de entorno PORT.
# Configuramos ASP.NET Core para que escuche en cualquier IP (0.0.0.0) en ese puerto.
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# =========================================================
# Etapa 2: Entorno de compilación (SDK)
# =========================================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto (.csproj) primero para aprovechar el caché de capas de Docker
COPY ["labo_10.sln", "./"]
COPY ["labo_10/labo_10.csproj", "labo_10/"]
COPY ["labo_10.Domain/labo_10.Domain.csproj", "labo_10.Domain/"]
COPY ["labo_10.Application/labo_10.Application.csproj", "labo_10.Application/"]
COPY ["labo_10.Infrastructure/labo_10.Infrastructure.csproj", "labo_10.Infrastructure/"]

# Restaurar las dependencias de NuGet de toda la solución
RUN dotnet restore "labo_10.sln"

# Copiar todo el resto del código fuente
COPY . .

# Compilar el proyecto principal (la API)
WORKDIR "/src/labo_10"
RUN dotnet build "labo_10.csproj" -c Release -o /app/build

# =========================================================
# Etapa 3: Publicación de la aplicación
# =========================================================
FROM build AS publish
RUN dotnet publish "labo_10.csproj" -c Release -o /app/publish /p:UseAppHost=false

# =========================================================
# Etapa 4: Imagen final ligera
# =========================================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "labo_10.dll"]