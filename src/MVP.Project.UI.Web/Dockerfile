#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MVP.Project.UI.Web/MVP.Project.UI.Web.csproj", "MVP.Project.UI.Web/"]
RUN dotnet restore "MVP.Project.UI.Web/MVP.Project.UI.Web.csproj"
COPY . .
WORKDIR "/src/MVP.Project.UI.Web"
RUN dotnet build "MVP.Project.UI.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MVP.Project.UI.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MVP.Project.UI.Web.dll"]
