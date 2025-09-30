FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet build -c Release

# =============================
# Test stage
# =============================
FROM build AS test
WORKDIR /app
CMD ["dotnet", "test", "AdvantageShoppingTests.csproj", "-c", "Release", "--filter", "Category=ParallelTests", "--logger:trx", "--results-directory", "/app/testresults"]
