#  Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /CurrencyConverterAPI

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /CurrencyConverterAPI

COPY --from=build /app/publish .

EXPOSE 80

ENV ConnectionStrings__DefaultConnection=""

ENTRYPOINT ["dotnet", "CurrencyConverterAPI.dll"]
