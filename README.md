# counter-culture.auth

This project was designed to facilitate bearer token authentication.
 
You can try it out using [Postman](https://learning.getpostman.com/). Please refer to the [API docs](https://documenter.getpostman.com/view/1403721/S17wPS3o).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://www.getpostman.com/collections/0dce1d0a523b04ee3cb3)

## Authentication

With the exception of `/api/v1/users/authenticate` and `/api/v1/users/login`, all endpoints require a valid bearer token. To learn how to get a token, see [Get a Token](https://github.com/mrtillman/counter-culture.docs/blob/master/auth/get-a-token.md).

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