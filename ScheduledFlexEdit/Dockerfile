FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ScheduledFlexEdit/ScheduledFlexEdit.csproj", "ScheduledFlexEdit/"]
RUN dotnet restore "ScheduledFlexEdit/ScheduledFlexEdit.csproj"
COPY . .
WORKDIR "/src/ScheduledFlexEdit"
RUN dotnet build "ScheduledFlexEdit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduledFlexEdit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduledFlexEdit.dll"]
