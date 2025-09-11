FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project
COPY AdvantageShoppingTests.sln ./
COPY AdvantageShoppingTests.csproj ./

# Copy everything else
COPY . .

# Restore dependencies
RUN dotnet restore AdvantageShoppingTests.sln

# Build
RUN dotnet build AdvantageShoppingTests.csproj -c Release -o /app/build

# =============================
# Test stage
# =============================
FROM build AS testrunner
WORKDIR /src
CMD ["dotnet", "test", "AdvantageShoppingTests.csproj", "-c", "Release", "--logger:trx", "--results-directory", "/app/testresults"]
