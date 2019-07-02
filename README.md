# counter-culture.secure

[![Build Status](https://travis-ci.com/mrtillman/counter-culture.secure.svg?branch=master)](https://travis-ci.com/mrtillman/counter-culture.secure)
[![Coverage Status](https://coveralls.io/repos/github/mrtillman/counter-culture.secure/badge.svg?branch=master)](https://coveralls.io/github/mrtillman/counter-culture.secure?branch=master)

---

This project was designed to facilitate bearer token authentication.
 
You can try it out using [Postman](https://learning.getpostman.com/). Please refer to the [API docs](https://documenter.getpostman.com/view/1403721/S1a7X6L7).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/0323d87983b842a1c15f)

## Authentication

With the exception of `/api/v1/oauth2/access_token`, all endpoints require a valid bearer token. To obtain a token, developers should first [register an app](https://geeks.counter-culture.io/register) and then [send an authorization request](https://github.com/mrtillman/counter-culture.docs/blob/master/secure/authorization-request.md).


## Getting Started

First, clone the Git repo:

```sh
git clone https://github.com/mrtillman/counter-culture.secure.git
```

Next, update `Server/appsettings.demo.json`<br/>
(rename this file to `appsettings.json`):

```sh
  ...

  "ConnectionStrings": {
    "DefaultMySQLConnection": "<DefaultMySQLConnection>",
    "DefaultRedisConnection": "<DefaultRedisConnection>"
  },
  "AppSecret": "the internet? is that thing still around?",
  "client_id": "<client_id>",
  "client_secret": "<client_secret>",
  "redirect_uri": "http://localhost:8080/oauth2/callback"
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