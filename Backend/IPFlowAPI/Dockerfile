FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet tool install --global dotnet-ef --version 8.0.0
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet ef migrations add InitialCreate || true

ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE $PORT

ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet run --no-launch-profile"]
