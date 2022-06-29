

## Install or Update dotnet EF Tools

dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

## Migrations

dotnet ef migrations add <migration-name>

dotnet ef database update

## MySql Docker Image

docker run --name mysql-dev -p 3306:3306 -e MYSQL_ROOT_PASSWORD=mysqlrootpassword -d mysql:8.0