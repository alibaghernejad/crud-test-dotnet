FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Mc2.CrudTest.Web/Mc2.CrudTest.Web.csproj", "src/Mc2.CrudTest.Web/"]
COPY ["src/Mc2.CrudTest.Application/Mc2.CrudTest.Application.csproj", "src/Mc2.CrudTest.Application/"]
COPY ["src/Mc2.CrudTest.Domain/Mc2.CrudTest.Domain.csproj", "src/Mc2.CrudTest.Domain/"]
COPY ["src/Mc2.CrudTest.Infrastructure/Mc2.CrudTest.Infrastructure.csproj", "src/Mc2.CrudTest.Infrastructure/"]
RUN dotnet restore "src/Mc2.CrudTest.Web/Mc2.CrudTest.Web.csproj"
COPY . .
WORKDIR "/src/src/Mc2.CrudTest.Web"
RUN dotnet build "Mc2.CrudTest.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mc2.CrudTest.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Web.dll"]
