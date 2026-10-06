FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet tool install --global dotnet-ef --version 8.0.0
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet ef migrations add InitialCreate || true
RUN dotnet ef bundle --self-contained -r linux-x64

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .
COPY --from=build /app/efbundle .

ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

ENTRYPOINT ["sh", "-c", "./efbundle && dotnet IPFlowAPI.dll"]
