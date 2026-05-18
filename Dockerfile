# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto usando curingas para evitar erros de caminhos de pastas
COPY *.csproj ./
RUN dotnet restore

# Copia todo o restante do código
COPY . .

# Compila o projeto diretamente no diretório atual
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# CONFIGURAÇÃO CRUCIAL PARA O RENDER:
# O Render exige a porta 10000 por padrão no plano Web Service
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestaoFinanceira.dll"]