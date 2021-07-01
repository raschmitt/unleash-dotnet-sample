FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["unleash-dotnet-sample.csproj", "./"]
RUN dotnet restore "unleash-dotnet-sample.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "unleash-dotnet-sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "unleash-dotnet-sample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "unleash-dotnet-sample.dll"]

RUN apt-get update && apt-get install -y curl
