# counter-culture.secure

A token server for [counter-culture.io](https://counter-culture.io).

---

[![Build Status](https://travis-ci.com/mrtillman/counter-culture.secure.svg?branch=master)](https://travis-ci.com/mrtillman/counter-culture.secure)
[![Coverage Status](https://coveralls.io/repos/github/mrtillman/counter-culture.secure/badge.svg?branch=master)](https://coveralls.io/github/mrtillman/counter-culture.secure?branch=master)
[![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/mrtillman/counter-culture.secure?sort=semver)](https://github.com/mrtillman/counter-culture.secure/releases/tag/v1.0.0-alpha)
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/mrtillman/counter-culture.secure/blob/master/LICENSE.md)
[![website-up](https://img.shields.io/website-up-down-green-red/http/shields.io.svg)](https://secure.counter-culture.io/)

## Getting Started

Counter-culture.secure is a REST API designed to issue, validate, renew and cancel OAuth 2.0 security tokens. All endpoints require a valid bearer token. To obtain a token, developers should first [register an app](https://geeks.counter-culture.io/register) and then [send an authorization request](https://github.com/mrtillman/counter-culture.secure/wiki/How-To-Send-an-Authorization-Request).

You can try it out using [Postman](https://learning.getpostman.com/). Please see the [API docs](https://documenter.getpostman.com/view/1403721/S1a7X6L7).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/0323d87983b842a1c15f)

### Prerequisites 

- .NET Core CLI
- MySQL
- [Entity Framework Core tools](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/)

### Installation

First, clone the repo:

```sh
git clone https://github.com/mrtillman/counter-culture.secure.git
```

Next, find `Presentation/appsettings.demo.json` and rename it to `appsettings.json`, then provide a value for the `AppSecret`:

```sh
  "AppSecret": "the internet? is that thing still around?"
```

> The `AppSecret` must be at least 32 characters long

### Database Setup

To prime your MySQL instance, run the script found at `Infrastructure/securedb.create.sql`. Once that's done, create a [standard connection string](https://www.connectionstrings.com/mysql-connector-net-mysqlconnection/standard) to the `secure` database. This is your `DefaultMySQLConnection`. Set this value in `appsettings.json` under `ConnectionStrings`:

```sh
"ConnectionStrings": {
    "DefaultMySQLConnection": "{DefaultMySQLConnection}",
 },
```

> Be sure that the database and user that appear in the `DefaultMySQLConnection` match the ones from `Infrastructure/securedb.create.sql`, otherwise you will receive an error when seeding the database.

To seed the `secure` database, visit `counter-culture/Presentation` from the command line and run each of the following [migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/) commands:

```sh
# create tables for oauth2 clients, scopes, resources and claims
$ dotnet ef database update -c ConfigurationDbContext

# create tables for authorization codes, refresh tokens, and reference tokens
$ dotnet ef database update -c PersistedGrantDbContext

# create asp.net identity tables for user management
$ dotnet ef database update -c SecureDbContext
```

You should now have a bunch of tables:

![oauth tables 1](https://raw.githubusercontent.com/mrtillman/counter-culture.secure/master/assets/secure.tables.1.png)

Tables not shown in previous image:

![oauth tables 2](https://raw.githubusercontent.com/mrtillman/counter-culture.secure/master/assets/secure.tables.2.png)

## Launching the Server

```sh
# let it rip
dotnet run -p Presentation/Presentation.csproj
```

### First-Party Clients

During the very first startup, counter-culture.secure registers [counter-culture.app](https://github.com/mrtillman/counter-culture.app) and [counter-culture.dev](https://github.com/mrtillman/counter-culture.dev) as first-party OAuth 2.0 clients. The `ClientId`  and `ClientSecret` for each app will be printed to the console, as shown in the example image below. Make note of these values as they appear in your terminal. You will need them to set up counter-culture.app and counter-culture.dev.

<!--TODO: move assets to Surge-->
![client creds output example](https://raw.githubusercontent.com/mrtillman/counter-culture.secure/master/assets/carbon.client.creds.png)

## Usage

Open http://localhost:5000/account/login to view it in the browser. You may log in using one of the following accounts:

|UserName|Password|
|---|---|
|barry.allen@example.com|WVPMHDma*kX6#JDV|
|bruce.banner@example.com|4tVz%JZD8huTR%gc|
|clark.kent@example.com|$3U%rhI30%K1je02|

## License
[MIT](https://github.com/mrtillman/counter-culture.secure/blob/master/LICENSE.md)

---

<h6 align="center">Don't write code. After all the code that you don't write is the easiest to produce, debug, and maintain. ~ Steve Oualline</h6>

---