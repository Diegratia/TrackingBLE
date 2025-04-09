FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/1.Auth/*.csproj ./
RUN dotnet restore


COPY src/1.Auth/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5001

ENTRYPOINT ["dotnet", "1.Auth.dll"]

