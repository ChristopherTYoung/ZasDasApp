FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

COPY . .

RUN dotnet workload install maui
RUN dotnet workload restore
CMD dotnet test