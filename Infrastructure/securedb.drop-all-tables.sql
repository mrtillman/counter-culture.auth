/*
SELECT concat('DROP TABLE IF EXISTS `', table_name, '`;')
FROM information_schema.tables
WHERE table_schema = 'secure';
*/

SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS `PersistedGrants`;
DROP TABLE IF EXISTS `IdentityResources`;
DROP TABLE IF EXISTS `IdentityProperties`;
DROP TABLE IF EXISTS `IdentityClaims`;
DROP TABLE IF EXISTS `DeviceCodes`;
DROP TABLE IF EXISTS `ClientSecrets`;
DROP TABLE IF EXISTS `ClientScopes`;
DROP TABLE IF EXISTS `Clients`;
DROP TABLE IF EXISTS `ClientRedirectUris`;
DROP TABLE IF EXISTS `ClientProperties`;
DROP TABLE IF EXISTS `ClientPostLogoutRedirectUris`;
DROP TABLE IF EXISTS `ClientIdPRestrictions`;
DROP TABLE IF EXISTS `ClientGrantTypes`;
DROP TABLE IF EXISTS `ClientCorsOrigins`;
DROP TABLE IF EXISTS `ClientClaims`;
DROP TABLE IF EXISTS `AspNetUserTokens`;
DROP TABLE IF EXISTS `AspNetUsers`;
DROP TABLE IF EXISTS `AspNetUserRoles`;
DROP TABLE IF EXISTS `AspNetUserLogins`;
DROP TABLE IF EXISTS `AspNetUserClaims`;
DROP TABLE IF EXISTS `AspNetRoles`;
DROP TABLE IF EXISTS `AspNetRoleClaims`;
DROP TABLE IF EXISTS `ApiSecrets`;
DROP TABLE IF EXISTS `ApiScopes`;
DROP TABLE IF EXISTS `ApiScopeClaims`;
DROP TABLE IF EXISTS `ApiResources`;
DROP TABLE IF EXISTS `ApiProperties`;
DROP TABLE IF EXISTS `ApiClaims`;
DROP TABLE IF EXISTS `__EFMigrationsHistory`;

SET FOREIGN_KEY_CHECKS = 1;