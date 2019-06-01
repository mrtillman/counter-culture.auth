FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

EXPOSE 5000

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "counter-culture.secure.server.dll", "--environment=Production"]