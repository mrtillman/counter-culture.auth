CREATE DATABASE secure;

USE secure;

-- root user for admin access
ALTER USER 'root' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON secure.* TO 'root'@'%';

-- user for entity framework connection
CREATE USER 'username'@'%' IDENTIFIED BY 'password';

ALTER USER 'username' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON secure.* TO 'username'@'%';