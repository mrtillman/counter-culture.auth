CREATE DATABASE secure;

USE secure;

-- root user for admin access
ALTER USER 'root' IDENTIFIED BY '{{root_password}}';
GRANT ALL PRIVILEGES ON secure.* TO 'root'@'%';

-- user for entity framework connection
CREATE USER '{{username}}'@'%' IDENTIFIED BY '{{password}}';

ALTER USER '{{username}}' IDENTIFIED BY '{{password}}';
GRANT ALL PRIVILEGES ON secure.* TO '{{username}}'@'%';

/*

to, seed this database:
0. open terminal
1. cd to counter-culture/secure/Server
2. run each:
    o. dotnet ef database update -c ConfigurationDbContext
    i. dotnet ef database update -c PersistedGrantDbContext
   ii. dotnet ef database update -c SecureDbContext

*/