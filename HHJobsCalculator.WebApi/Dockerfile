#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HHJobsCalculator.WebApi/HHJobsCalculator.WebApi.csproj", "HHJobsCalculator.WebApi/"]
COPY ["HHJobsCalculator.Engine/HHJobsCalculator.Engine.csproj", "HHJobsCalculator.Engine/"]
COPY ["HHJobsCalculator.Core/HHJobsCalculator.Core.csproj", "HHJobsCalculator.Core/"]
RUN dotnet restore "HHJobsCalculator.WebApi/HHJobsCalculator.WebApi.csproj"
COPY . .
WORKDIR "/src/HHJobsCalculator.WebApi"
RUN dotnet build "HHJobsCalculator.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HHJobsCalculator.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HHJobsCalculator.WebApi.dll"]