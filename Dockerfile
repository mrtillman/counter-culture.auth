FROM microsoft/dotnet:sdk AS build-env
WORKDIR /counter-culture.secure

ENV ASPNETCORE_ENVIRONMENT=Development

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

EXPOSE 5000

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /counter-culture.secure
COPY --from=build-env /counter-culture.secure/Presentation/out .

ENTRYPOINT [ "dotnet", "Presentation.dll", "--environment=Development"]