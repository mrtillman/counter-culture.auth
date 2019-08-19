# counter-culture.secure

A token server for [counter-culture.io](https://counter-culture.io).

---

[![Build Status](https://travis-ci.com/mrtillman/counter-culture.secure.svg?branch=master)](https://travis-ci.com/mrtillman/counter-culture.secure)
[![Coverage Status](https://coveralls.io/repos/github/mrtillman/counter-culture.secure/badge.svg?branch=master)](https://coveralls.io/github/mrtillman/counter-culture.secure?branch=master)

## Getting Started

Counter-culture.secure is a REST API designed to issue, validate, renew and cancel OAuth 2.0 security tokens. All endpoints require a valid bearer token. To obtain a token, developers should first [register an app](https://geeks.counter-culture.io/register) and then [send an authorization request](https://github.com/mrtillman/counter-culture.docs/blob/master/secure/authorization-request.md).
 
You can try it out using [Postman](https://learning.getpostman.com/). Please refer to the [API docs](https://documenter.getpostman.com/view/1403721/S1a7X6L7).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/0323d87983b842a1c15f)

### Prerequisites 

Be sure to install the [.NET CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x), as counter-culture.secure is a .NET Core 2.2 Web API project.

This project also requires a MySQL instance.

### Installation

First, clone the repo:

```sh
git clone https://github.com/mrtillman/counter-culture.secure.git
```

Next, find `Server/appsettings.demo.json` and rename it to `appsettings.json`, then provide a value for the `AppSecret`:

```sh
  "AppSecret": "the internet? is that thing still around?"
```

> It is important to note that the `AppSecret` must be at least 32 characters long - it must also match the secret from [counter-culture.api](https://github.com/mrtillman/counter-culture.api).

### Database Setup

To prime your MySQL instance, run the script found at `Server/Configuration/securedb.create.sql`. Once that's done, create a [standard connection string](https://www.connectionstrings.com/mysql-connector-net-mysqlconnection/standard/) to the `secure` database. This is your `DefaultMySQLConnection`. Set this value in `appsettings.json` under `ConnectionStrings`:

```sh
"ConnectionStrings": {
    "DefaultMySQLConnection": "{DefaultMySQLConnection}",
 },
```

> Be sure that the databse and user that appear in the `DefaultMySQLConnection` match the ones from `Server/Configuration/securedb.create.sql`, otherwise you will receive an error when seeding the database.

To seed the `secure` database, visit `counter-culture/secure/Server` from the command line and run each of the following [migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/) commands:

```sh
# create tables for oauth2 clients, scopes, resources and claims
dotnet ef database update -c ConfigurationDbContext

# create tables for authorization codes, refresh tokens, and reference tokens
dotnet ef database update -c PersistedGrantDbContext

# create asp.net identity tables for user management
dotnet ef database update -c SecureDbContext
```

You should now have a bunch of tables:

![oauth flow](https://github.com/mrtillman/counter-culture.secure/blob/dev/assets/secure.tables.1.png)

Tables not shown in previous image:

![oauth flow](https://github.com/mrtillman/counter-culture.secure/blob/dev/assets/secure.tables.2.png)

## Launching the Server

```sh
# enter the project root
cd counter-culture.secure/Server

# let it rip
dotnet run
```

## Usage

Open http://localhost:5000/account/login to view it in the browser. You may log in using one of the following accounts:

|UserName|Password|
|---|---|
|barry.allen@example.com|WVPMHDma*kX6#JDV|
|bruce.banner@example.com|4tVz%JZD8huTR%gc|
|clark.kent@example.com|$3U%rhI30%K1je02|

## License
[MIT](https://github.com/mrtillman/counter-culture.secure/blob/master/LICENSE.md)