# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем решение и проекты
COPY GladiatorsOnline.sln ./
COPY Presentation/*.csproj ./Presentation/
COPY Business/*.csproj ./Business/
COPY Data/*.csproj ./Data/

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем весь исходный код
COPY Presentation/. ./Presentation/
COPY Business/. ./Business/
COPY Data/. ./Data/

# Билдим Presentation (API)
WORKDIR /app/Presentation
RUN dotnet publish -c Release -o /app/publish

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5180
ENTRYPOINT ["dotnet", "Presentation.dll"]