
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Eshop.Web/Eshop.Web.csproj", "Eshop.Web/"]
COPY ["Eshop.Domain/Eshop.Domain.csproj", "Eshop.Domain/"]
COPY ["Eshop.Repository/Eshop.Repository.csproj", "Eshop.Repository/"]
COPY ["Eshop.Service/Eshop.Service.csproj", "Eshop.Service/"]
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
