## Install or Update .NET EF Tools

```sh
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

## Migrations

### Add New Migration
```sh
dotnet ef migrations add <migration-name>
```

### Update Dabase
```sh
dotnet ef database update
```

## Run MySql Docker

```sh
docker run --name mysql-dev -p 3306:3306 -e MYSQL_ROOT_PASSWORD=mysqlrootpassword -d mysql:8.0
```