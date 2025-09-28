# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo los archivos necesarios para restaurar dependencias
COPY WebApiDemo/*.csproj WebApiDemo/
COPY *.sln ./

# Restaurar dependencias
RUN dotnet restore WebApiDemo/WebApiDemo.csproj

# Copiar el resto del código fuente
COPY . .

# Publicar la aplicación
RUN dotnet publish WebApiDemo/WebApiDemo.csproj -c Release -o /app/publish

# Etapa 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Crear usuario no-root
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

WORKDIR /app
COPY --from=build /app/publish .

# Cambiar al usuario seguro
USER appuser

EXPOSE 80
ENTRYPOINT ["dotnet", "WebApiDemo.dll"]
