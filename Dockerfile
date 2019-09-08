FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build

# copy csproj and restore as distinct layers
WORKDIR /app
COPY backend/*.csproj ./backend/
WORKDIR /app/backend
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
COPY backend/. ./backend/
WORKDIR /app/backend
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
WORKDIR /app
COPY --from=build /app/backend/out ./
ENTRYPOINT ["dotnet", "Caps.dll"]
