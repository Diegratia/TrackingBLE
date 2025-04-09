FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY src/20.VisitorBlacklistArea/*.csproj ./
COPY src/Common/ ./Common/
RUN dotnet restore


COPY src/20.VisitorBlacklistArea/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5020
ENTRYPOINT ["dotnet", "20.VisitorBlacklistArea.dll"] 