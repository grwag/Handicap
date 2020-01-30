#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
# install nodejs
RUN apt-get update \
  && apt-get -y install curl gnupg \
  && curl -sL https://deb.nodesource.com/setup_12.x  | bash - \
  && apt-get -y install nodejs

WORKDIR /src
COPY . .
WORKDIR "/src/Handicap.Api"
RUN dotnet build "Handicap.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Handicap.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Handicap.Api.dll"]