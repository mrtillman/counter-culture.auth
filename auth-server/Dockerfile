FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY . /app
WORKDIR /app

# Restore and Build.
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

# Container will open port 5000 allowing us to connect to it.
EXPOSE 5000

# Start the dotnet application on port 5000.
CMD ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]

