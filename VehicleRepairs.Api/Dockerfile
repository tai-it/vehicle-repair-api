#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
WORKDIR /app
COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet VehicleRepairs.Api.dll