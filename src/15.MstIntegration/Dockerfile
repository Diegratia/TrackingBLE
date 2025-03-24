FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/15.MstIntegration/*.csproj ./
COPY src/Common/ ./Common/
RUN dotnet restore

COPY src/15.MstIntegration/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5015
ENTRYPOINT ["dotnet", "15.MstIntegration.dll"] 