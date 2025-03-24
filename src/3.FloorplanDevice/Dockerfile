FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/3.FloorplanDevice/*.csproj ./
COPY src/Common/ ./Common/
RUN dotnet restore

COPY src/3.FloorplanDevice/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5003

ENTRYPOINT ["dotnet", "3.FloorplanDevice.dll"]