FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
WORKDIR /app
COPY bin/Release/netcoreapp3.1/publish/ ./
ENTRYPOINT ["dotnet", "Denggaopan.K8sConfigMapDemo.dll"]