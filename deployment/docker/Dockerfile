FROM mcr.microsoft.com/dotnet/aspnet:7.0

ARG ProjectName
ENV AssemblyName=$ProjectName.dll

WORKDIR /App

COPY publish/$ProjectName/* .

ENTRYPOINT dotnet $AssemblyName
