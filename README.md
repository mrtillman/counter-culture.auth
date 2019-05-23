# counter-culture.secure

[![Build Status](https://travis-ci.com/mrtillman/counter-culture.secure.svg?branch=master)](https://travis-ci.com/mrtillman/counter-culture.secure)
[![Coverage Status](https://coveralls.io/repos/github/mrtillman/counter-culture.secure/badge.svg?branch=master)](https://coveralls.io/github/mrtillman/counter-culture.secure?branch=master)

---

This project was designed to facilitate bearer token authentication.
 
You can try it out using [Postman](https://learning.getpostman.com/). Please refer to the [API docs](https://documenter.getpostman.com/view/1403721/S17wPS3o).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://www.getpostman.com/collections/0dce1d0a523b04ee3cb3)

## Authentication

With the exception of `/api/v1/oauth2/access_token`, all endpoints require a valid bearer token. To obtain a token, developers should first [register an app](https://github.com/mrtillman/counter-culture.docs/blob/master/secure/register-app.md) and then [send an authorization request](https://github.com/mrtillman/counter-culture.docs/blob/master/secure/authorization-request.md).


## Getting Started

First, clone the Git repo:

```sh
git clone https://github.com/mrtillman/counter-culture.secure.git
```

Next, update `appsettings.json`:

```sh
  ...

  "ConnectionStrings": {
    "DefaultMySQLConnection": "<DefaultMySQLConnection>",
    "DefaultRedisConnection": "<DefaultRedisConnection>"
  },
  "AppSecret": "the internet? is that thing still around?",
  "ccult_client_id": "<ccult_client_id>",
  "ccult_client_secret": "<ccult_client_secret>",
  "ccult_redirect_uri": "http://localhost:8080/oauth2/callback"
  }

}
```

> It is important to note that the `AppSecret` must be at least 32 characters long - it must also match the secret from [counter-culture.api](https://github.com/mrtillman/counter-culture.api).

Start the server:

```sh
cd counter-culture.secure/Server
dotnet run
```

Open [http://localhost:5000](http://localhost:5000) to view it in the browser.