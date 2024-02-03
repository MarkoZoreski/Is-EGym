
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY .
RUN dotnet restore "Eshop.Web/Eshop.Web.csproj"
COPY . .
WORKDIR "/src/Eshop.Web"
RUN dotnet build "Eshop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eshop.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.Web.dll"]
