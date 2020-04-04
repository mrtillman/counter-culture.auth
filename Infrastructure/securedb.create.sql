CREATE DATABASE secure;

-- database for the DefaultMySQLConnection from appsettings.json
USE secure;

-- root user for admin access
ALTER USER 'root' IDENTIFIED BY '{{root_password}}';
GRANT ALL PRIVILEGES ON secure.* TO 'root'@'%';

-- user for the DefaultMySQLConnection from appsettings.json
CREATE USER '{{username}}'@'%' IDENTIFIED BY '{{password}}';

ALTER USER '{{username}}' IDENTIFIED BY '{{password}}';
GRANT ALL PRIVILEGES ON secure.* TO '{{username}}'@'%';
