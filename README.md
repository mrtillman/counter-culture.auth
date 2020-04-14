# counter-culture.secure

A token server for [counter-culture.io](https://counter-culture.io).

---

[![Build Status](https://travis-ci.com/mrtillman/counter-culture.secure.svg?branch=master)](https://travis-ci.com/mrtillman/counter-culture.secure)
[![Coverage Status](https://coveralls.io/repos/github/mrtillman/counter-culture.secure/badge.svg?branch=master)](https://coveralls.io/github/mrtillman/counter-culture.secure?branch=master)
[![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/mrtillman/counter-culture.secure?sort=semver)](https://github.com/mrtillman/counter-culture.secure/releases/tag/v1.0.0-alpha)
[![license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/mrtillman/counter-culture.secure/blob/master/LICENSE.md)
[![website-up](https://img.shields.io/website-up-down-green-red/http/shields.io.svg)](https://secure.counter-culture.io/)

## Getting Started

**`counter-culture.secure`** is a REST API designed to issue, validate, renew and cancel OAuth 2.0 security tokens. All endpoints require a valid bearer token. To obtain a token, developers should first [register an app](https://geeks.counter-culture.io/register) and then [send an authorization request](https://github.com/mrtillman/counter-culture.secure/wiki/How-To-Send-an-Authorization-Request).

You can try it out using [Postman](https://learning.getpostman.com/). Please see the [API docs](https://documenter.getpostman.com/view/1403721/S1a7X6L7).

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/0323d87983b842a1c15f)

### Prerequisites 

- .NET Core CLI
- MySQL

### Installation

First, clone the repo:

```sh
git clone https://github.com/mrtillman/counter-culture.secure.git
```

Next, find `Presentation/appsettings.demo.json` and rename it to `appsettings.json`, then provide a value for the `AppSecret`:

```sh
  "AppSecret": "the internet? is that thing still around?"
```

> Note that the `AppSecret` must be at least 32 characters long

### Database Setup

To prime your MySQL instance, execute `Infrastructure/securedb.create.sql`. Once that's done, create a [standard connection string](https://www.connectionstrings.com/mysql-connector-net-mysqlconnection/standard) to the `secure` database. This is your `DefaultMySQLConnection`. Set this value in `appsettings.json` under `ConnectionStrings`:

```sh
"ConnectionStrings": {
    "DefaultMySQLConnection": "{DefaultMySQLConnection}",
 },
```

> Be sure that the database and user that appear in the `DefaultMySQLConnection` match the ones from `Infrastructure/securedb.create.sql`, otherwise you will receive an error when creating the database schema.

To create the database schema, execute the following [EF Migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) scripts:

```sh
$ cd Presentation
$ ./add-migrations.sh
$ ./apply-migrations.sh
```

You should now have the following tables:

```sh
ApiProperties
ApiResources
ApiScopeClaims
ApiScopes
ApiSecrets
AspNetRoleClaims
AspNetRoles
AspNetUserClaims
AspNetUserLogins
AspNetUserRoles
AspNetUserTokens
AspNetUsers
ClientClaims
ClientCorsOrigins
ClientGrantTypes
ClientIdPRestrictions
ClientPostLogoutRedirectUris
ClientProperties
ClientRedirectUris
ClientScopes
ClientSecrets
Clients
DeviceCodes
IdentityClaims
IdentityProperties
IdentityResources
PersistedGrants
__EFMigrationsHistory
```

## Launching the Server

```sh
# let it rip
$ dotnet run -p Presentation/Presentation.csproj
```

### First-Party Clients

During the very first startup, **`counter-culture.secure`** registers [counter-culture.api](https://github.com/mrtillman/counter-culture.api), [counter-culture.app](https://github.com/mrtillman/counter-culture.app) and [counter-culture.dev](https://github.com/mrtillman/counter-culture.dev) as first-party OAuth 2.0 clients. The ID and secret for each client will be written to
`Presentation/clients.env`. Make note of these values, as you will need them to set up counter-culture.api, counter-culture.app and counter-culture.dev.

## Usage

Open http://counter-culture:5000/account/login to view it in the browser. You may sign in using one of the following demo accounts:

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
