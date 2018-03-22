# .Net Core Test Project

## Local Project Deployment

### Fixing DB initialization with MySQL provider

MySQL provider for .Net failes to initially creatre MigrationHistory table, so you have to do it manually.

For that purpose run the script bellow:

```
CREATE TABLE `__EFMigrationsHistory`
(
    `MigrationId` nvarchar(150) NOT NULL,
    `ProductVersion` nvarchar(32) NOT NULL,
    
    PRIMARY KEY (`MigrationId`)
);
```

After the MigrationHistory table created apply other migrations:

```
update-database
```

### Facebook Authentification

To make the Facebook Authentification work proceed with this steps:

* Enable SSL (.csproj Properties -> Debug)
* Add *Authentication:Facebook:AppId* and *Authentication:Facebook:AppSecret* keys to secrets.json

