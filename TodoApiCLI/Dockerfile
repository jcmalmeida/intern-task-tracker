FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ./TodoApi ./TodoApi
COPY ./TodoApiCLI ./TodoApiCLI

WORKDIR /app/TodoApiCLI
RUN dotnet restore

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /out .

ENTRYPOINT ["dotnet", "TodoApiCLI.dll"]
