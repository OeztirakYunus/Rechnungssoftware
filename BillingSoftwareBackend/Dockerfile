FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BillingSoftware.Web/BillingSoftware.Web.csproj", "BillingSoftware.Web/"]
COPY ["BillingSoftware.Core/BillingSoftware.Core.csproj", "BillingSoftware.Core/"]
COPY ["BillingSoftware.Persistence/BillingSoftware.Persistence.csproj", "BillingSoftware.Persistence/"]
COPY ["CommonBase/CommonBase.csproj", "CommonBase/"]
RUN dotnet restore "BillingSoftware.Web/BillingSoftware.Web.csproj"
COPY . .
RUN dotnet publish  "BillingSoftware.Web/BillingSoftware.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build /app/publish .
COPY ./BillingSoftware.Web/templates/ ./templates/.
ENTRYPOINT ["dotnet", "BillingSoftware.Web.dll"]
