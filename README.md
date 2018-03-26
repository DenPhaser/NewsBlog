# .Net Core Test Project

## TODO:

* **[InProgress]** Functional requirements for the system is attached
* **[InProgress]** Use Admin LTE UI framework for UI layer
* System shall have 75% or more unit-tests coverage
* System shall have integration tests coverage
* **[Done]** System shall allow logging using Facebook account
* System shall be covered with static analysis of the C# and JS (if any) code
* System build shall be fully automated and implemented via Gitlab build pipelines
* **[InProgress]** Use MySQL as DB and Debian as hosting OS

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

