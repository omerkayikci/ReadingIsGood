FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build

WORKDIR /workspace
COPY . .

WORKDIR /workspace/src/ReadingIsGood.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.2-alpine3.11 AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "ReadingIsGood.Api.dll"]
