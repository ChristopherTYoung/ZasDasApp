FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

COPY . .


CMD dotnet test ZasAndDasMobile.Tests
