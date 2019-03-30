# counter-culture.auth

This project was designed to facilitate bearer token authentication.

## Getting Started

First, clone the Git repo:

```sh
git clone https://github.com/mrtillman/counter-culture.auth.git
```

Next, start the auth server:

```sh
cd counter-culture.auth
dotnet restore
dotnet run
```

## Get Access Token

|Method|URL|
|---|---|
|POST|http://localhost:5000/api/v1/users/authenticate|

Response:

```sh
{
    "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI1IiwidW5pcXVlX25hbWUiOiJwZXRlciIsIm5iZiI6MTU1MzkwODY5OSwiZXhwIjoxNTUzOTk1MDk5LCJpYXQiOjE1NTM5MDg2OTl9.OsGH5S2fCj_5t2bq5NFhnG7QqK3nz9O4O0pCyl-B8p8",
    "token_type": "bearer",
    "expires_in": 86399,
    "expiration_date": "2019-03-31T01:18:19.8646858Z",
    "refresh_token": null
}
```

## Form Authentication
Open [http://localhost:5000](http://localhost:5000) to view the login page in the browser. Upon successful login, the page will redirect to the app, so be sure [counter-culture.app](https://github.com/mrtillman/counter-culture.app) is running at [http://localhost:8080](http://localhost:8080).

## Deployment

Sign in to the remote server and clone the repo:

```sh
ssh user@counter-culture.io
cd ~/auth
git clone https://github.com/mrtillman/counter-culture.auth.git
```

Next, use the production launch settings:

```sh
cd Properties/
mv launchSettings.prod.json launchSettings.json
cd ..
```

Create `appsecrets.json` in the project root:
```sh
{
  "MySQLConnectionString": "<MySQLConnectionString>",
  "Secret": "<app-secret>"
}
```

Containerize the auth server:

```sh
docker build -t counter-culture.auth .
```

Start the container on the remote production server:

```sh
docker run -p 5000:5000 -d username/counter-culture.auth
```

For more details, see the [Frequently used Docker CLI commands](https://github.com/mrtillman/counter-culture.docs/blob/master/docker/cli-commands.md).
