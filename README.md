# counter-culture.auth

This project was designed to facilitate bearer token authentication.
 
You can try it out using [Postman](https://learning.getpostman.com/). Please refer to the [API docs](https://documenter.getpostman.com/view/1403721/S17wPS3o).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://www.getpostman.com/collections/0dce1d0a523b04ee3cb3)

## Authentication

With the exception of `` and ``, All API requests must include a valid bearer token.<br/>
To learn how to get a token, see [Get a Token](https://github.com/mrtillman/counter-culture.docs/blob/master/auth/get-a-token.md).

## Getting Started

First, clone the Git repo:

```sh
git clone https://github.com/mrtillman/counter-culture.auth.git
```

Next, create `appsecrets.json` in the project root:
```sh
{
  "MySQLConnectionString": "<MySQLConnectionString>",
  "Secret": "<app-secret>"
}
```

Start the auth server:

```sh
cd counter-culture.auth
dotnet restore
dotnet build
dotnet run
```

Open [http://localhost:5000](http://localhost:5000) to view it in the browser. Upon successful login, the page will redirect to http://localhost:8080, so be sure [counter-culture.app](https://github.com/mrtillman/counter-culture.app) is up and running on port 8080.

## Deployment

Sign in to the remote server and clone the repo:

```sh
ssh username@counter-culture.io
cd ~/auth
git clone https://github.com/mrtillman/counter-culture.auth.git
```

Remember to create `appsecrets.json`.

Next, use the production launch settings:

```sh
cd Properties/
mv launchSettings.prod.json launchSettings.json
cd ..
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
