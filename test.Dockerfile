FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

COPY . .

RUN dotnet workload restore --version net9.0
CMD dotnet test
