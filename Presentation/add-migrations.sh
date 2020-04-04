# http://docs.identityserver.io/en/release/quickstarts/8_entity_framework.html#adding-migrations

dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb && \
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb && \
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c SecureDbContext -o Data/Migrations/IdentityServer/SecureDb
