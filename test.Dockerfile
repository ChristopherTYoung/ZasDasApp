FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

COPY . .

RUN dotnet workload restore --ignore-failed-sources
CMD dotnet test
