FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos .csproj
COPY ["MVP.Project.Services.Api/MVP.Project.Services.Api.csproj", "MVP.Project.Services.Api/"]
COPY ["MVP.Project.Application/MVP.Project.Application.csproj", "MVP.Project.Application/"]
COPY ["MVP.Project.Domain/MVP.Project.Domain.csproj", "MVP.Project.Domain/"]
COPY ["MVP.Project.Domain.Core/MVP.Project.Domain.Core.csproj", "MVP.Project.Domain.Core/"]
COPY ["MVP.Project.Infra.Data/MVP.Project.Infra.Data.csproj", "MVP.Project.Infra.Data/"]
COPY ["MVP.Project.Infra.CrossCutting.IoC/MVP.Project.Infra.CrossCutting.IoC.csproj", "MVP.Project.Infra.CrossCutting.IoC/"]
COPY ["MVP.Project.Infra.CrossCutting.Bus/MVP.Project.Infra.CrossCutting.Bus.csproj", "MVP.Project.Infra.CrossCutting.Bus/"]
COPY ["MVP.Project.Infra.CrossCutting.Identity/MVP.Project.Infra.CrossCutting.Identity.csproj", "MVP.Project.Infra.CrossCutting.Identity/"]

# Restaurar dependências
RUN dotnet restore "MVP.Project.Services.Api/MVP.Project.Services.Api.csproj"

# Copiar todo o código-fonte
COPY . .

# Compilar o projeto
WORKDIR "/src/MVP.Project.Services.Api"
RUN dotnet build "MVP.Project.Services.Api.csproj" -c Release -o /app/build

# Publicar o projeto
FROM build AS publish
RUN dotnet publish "MVP.Project.Services.Api.csproj" -c Release -o /app/publish

# Configurar a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVP.Project.Services.Api.dll"]