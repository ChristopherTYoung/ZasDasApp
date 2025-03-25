FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

COPY . .

RUN dotnet workload install maui --source https://aka.ms/dotnet6/nuget/index.json --source https://api.nuget.org/v3/index.json
RUN dotnet workload install android ios maccatalyst tvos macos maui wasm-tools --source https://aka.ms/dotnet6/nuget/index.json --source https://api.nuget.org/v3/index.json
RUN dotnet workload restore
CMD dotnet test