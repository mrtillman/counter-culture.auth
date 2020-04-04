# https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-3.1

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /counter-culture.secure

ENV ASPNETCORE_ENVIRONMENT=Development

COPY . ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish -c Release -o ./out/

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /counter-culture.secure
COPY --from=build-env /counter-culture.secure/out .

ENTRYPOINT [ "dotnet", "Presentation.dll", "--environment=Development"]