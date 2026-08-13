# ---------- Etapa 1: compilar y publicar ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Primero solo el csproj: si no cambia, Docker reutiliza el restore.
COPY CafeLab.Api/CafeLab.Api.csproj CafeLab.Api/
RUN dotnet restore CafeLab.Api/CafeLab.Api.csproj

# Ahora el resto del codigo fuente.
COPY . .
RUN dotnet publish CafeLab.Api/CafeLab.Api.csproj -c Release -o /app/publish

# ---------- Etapa 2: imagen final de ejecucion ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CafeLab.Api.dll"]