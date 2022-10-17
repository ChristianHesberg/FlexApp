FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ScheduledFlexEditor/ScheduledFlexEditor.csproj", "ScheduledFlexEditor/"]
RUN dotnet restore "ScheduledFlexEditor/ScheduledFlexEditor.csproj"
COPY . .
WORKDIR "/src/ScheduledFlexEditor"
RUN dotnet build "ScheduledFlexEditor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduledFlexEditor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduledFlexEditor.dll"]
