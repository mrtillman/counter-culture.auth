# counter-culture.secure

A token server for [counter-culture.io](https://counter-culture.io).

---

[![Build Status](https://travis-ci.com/mrtillman/counter-culture.secure.svg?branch=master)](https://travis-ci.com/mrtillman/counter-culture.secure)
[![Coverage Status](https://coveralls.io/repos/github/mrtillman/counter-culture.secure/badge.svg?branch=master)](https://coveralls.io/github/mrtillman/counter-culture.secure?branch=master)

## Getting Started

Counter-culture.secure is a REST API designed to issue, validate, renew and cancel OAuth 2.0 security tokens.
 
You can try it out using [Postman](https://learning.getpostman.com/). Please refer to the [API docs](https://documenter.getpostman.com/view/1403721/S1a7X6L7).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/0323d87983b842a1c15f)

### Prerequisites 

All endpoints require a valid bearer token. To obtain a token, developers should first [register an app](https://geeks.counter-culture.io/register) and then [send an authorization request](https://github.com/mrtillman/counter-culture.docs/blob/master/secure/authorization-request.md).

Counter-culture.secure also requires a MySQL instance and a Redis instance. 

### Installation

First, clone the repo:

```sh
git clone https://github.com/mrtillman/counter-culture.secure.git
```

Next, find `Server/appsettings.demo.json` and rename it to `appsettings.json`:

```sh
  ...

  "ConnectionStrings": {
    "DefaultMySQLConnection": "<DefaultMySQLConnection>",
    "DefaultRedisConnection": "<DefaultRedisConnection>"
  },
  
  "AppSecret": "the internet? is that thing still around?"

}
```

> It is important to note that the `AppSecret` must be at least 32 characters long - it must also match the secret from [counter-culture.api](https://github.com/mrtillman/counter-culture.api).

Start the server:

```sh
# enter project root
cd counter-culture.secure/Server

# let it rip
dotnet run
```

### First-Party Clients

At startup, counter-culture.secure registers [counter-culture.app](https://github.com/mrtillman/counter-culture.app) and [counter-culture.dev](https://github.com/mrtillman/counter-culture.dev) as OAuth 2.0 clients. To obtain the `client_id` and `client_secret` that belong to each app, use a MySQL tool such as [Workbench](https://dev.mysql.com/downloads/workbench/) to query the `oauth_clients` table:

```sql
SELECT * FROM oauth_clients;
```

## Usage

Open [http://localhost:5000](http://localhost:5000) to view it in the browser.

## License
[MIT](https://github.com/mrtillman/counter-culture.secure/blob/master/LICENSE.md)