﻿FROM mcr.microsoft.com/dotnet/aspnet:latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /src
COPY ["WordCount.csproj", "./"]
RUN dotnet restore "WordCount.csproj"
COPY . .
#WORKDIR "/src/"
#RUN dotnet build "WordCount.csproj" -c Release -o /app/build

#FROM build AS publish
RUN dotnet publish "WordCount.csproj" -o /app/publish

FROM base AS final
WORKDIR /app
#COPY --from=publish /app/publish .
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WordCount.dll"]
