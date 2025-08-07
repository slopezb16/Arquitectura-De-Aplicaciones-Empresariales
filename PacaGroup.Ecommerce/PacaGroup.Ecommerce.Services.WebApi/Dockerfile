## Etapa 1: Build con .NET 6 SDK
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#WORKDIR /src
#
## Copiamos todo y publicamos
#COPY . . 
#WORKDIR /src/PacaGroup.Ecommerce.Services.WebApi
#RUN dotnet restore
#RUN dotnet publish -c Release -o /out
##RUN dotnet publish -c Release -o /out --no-restore --verbosity minimal -f net6.0
#
## Etapa 2: Runtime con ASP.NET 6
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /app
##COPY --from=build-env /out ./
#COPY --from=build-env /src/PacaGroup.Ecommerce.Services.WebApi/out ./
#
## Puerto expuesto por defecto
##EXPOSE 80
#
## Comando de inicio
##ENTRYPOINT ["dotnet", "MiApiDocker.dll"]
#ENTRYPOINT ["dotnet", "PacaGroup.Ecommerce.Services.WebApi.dll"]
##

# Etapa 1: Build con .NET 6 SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

# Copiamos todo y publicamos
COPY . . 
WORKDIR /src/PacaGroup.Ecommerce.Services.WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o /out --no-restore --verbosity minimal -f net6.0

# Etapa 2: Runtime con ASP.NET 6
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Instalamos las dependencias necesarias globalmente
RUN apt-get update && apt-get install -y \
    dotnet-sdk-6.0 \
    dotnet-framework-4.7.2-dev

WORKDIR /app

# Copiamos los archivos publicados desde la etapa de build
COPY --from=build-env /src/PacaGroup.Ecommerce.Services.WebApi/out ./

# Puerto expuesto por defecto
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "PacaGroup.Ecommerce.Services.WebApi.dll"]
