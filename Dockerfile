# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia o arquivo .csproj de dentro da subpasta GestaoFinanceira
COPY ["GestaoFinanceira/GestaoFinanceira.csproj", "GestaoFinanceira/"]

# 2. Executa o restore apontando diretamente para o arquivo copiado
RUN dotnet restore "GestaoFinanceira/GestaoFinanceira.csproj"

# 3. Copia todo o resto dos arquivos do repositório
COPY . .

# 4. Entra na subpasta onde o código principal está antes de compilar
WORKDIR "/src/GestaoFinanceira"
RUN dotnet publish "GestaoFinanceira.csproj" -c Release -o /app/publish

# --- Estágio Final ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestaoFinanceira.dll"]