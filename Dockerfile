FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app

COPY DevOpsAssignment/DevOpsAssignment.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DevOpsAssignment.dll"]