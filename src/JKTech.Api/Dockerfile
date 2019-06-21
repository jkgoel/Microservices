FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/JKTech.Api/*.csproj", "src/JKTech.Api/"]
COPY ["src/JKTech.Common/*.csproj", "src/JKTech.Common/"]
RUN dotnet restore "./src/JKTech.Api/JKTech.Api.csproj"
COPY . .
WORKDIR "./src/JKTech.Api"
RUN dotnet build "JKTech.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JKTech.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JKTech.Api.dll"]