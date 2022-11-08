#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Fithub-API/Fithub-API.csproj", "Fithub-API/"]
COPY ["Fithub-BL/Fithub-BL.csproj", "Fithub-BL/"]
COPY ["Fithub-Data/Fithub-Data.csproj", "Fithub-Data/"]
COPY ["Fithub-DL/Fithub-DL.csproj", "Fithub-DL/"]
RUN dotnet restore "Fithub-API/Fithub-API.csproj"
COPY . .
WORKDIR "/src/Fithub-API"
RUN dotnet build "Fithub-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fithub-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fithub-API.dll"]