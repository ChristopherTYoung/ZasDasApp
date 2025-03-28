FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /ZasAndDasWeb
COPY . ./
RUN dotnet publish ZasAndDasWeb -o out
EXPOSE 8080
ENTRYPOINT ["dotnet", "out/ZasAndDasWeb.dll"]
