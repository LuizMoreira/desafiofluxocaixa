#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443
ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Services/FluxoCaixa/Microservices.FluxoCaixa.API/Microservices.FluxoCaixa.API.csproj", "Services/FluxoCaixa/Microservices.FluxoCaixa.API/"]
COPY ["Common/Common.Logger/Common.Logger.csproj", "Common/Common.Logger/"]
COPY ["Common/Event.Messages/Event.Messages.csproj", "Common/Event.Messages/"]
COPY ["Services/FluxoCaixa/Microservices.FluxoCaixa.Application/Microservices.FluxoCaixa.Application.csproj", "Services/FluxoCaixa/Microservices.FluxoCaixa.Application/"]
COPY ["Services/FluxoCaixa/Microservices.FluxoCaixa.Core/Microservices.FluxoCaixa.Core.csproj", "Services/FluxoCaixa/Microservices.FluxoCaixa.Core/"]
COPY ["Services/FluxoCaixa/Microservices.FluxoCaixa.Infrastructure/Microservices.FluxoCaixa.Infrastructure.csproj", "Services/FluxoCaixa/Microservices.FluxoCaixa.Infrastructure/"]
RUN dotnet restore "Services/FluxoCaixa/Microservices.FluxoCaixa.API/Microservices.FluxoCaixa.API.csproj"
COPY . .
WORKDIR "/src/Services/FluxoCaixa/Microservices.FluxoCaixa.API"
RUN dotnet build "Microservices.FluxoCaixa.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.FluxoCaixa.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.FluxoCaixa.API.dll"]