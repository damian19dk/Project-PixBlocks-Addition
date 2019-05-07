# Project-PixBlocks-Addition
ReadMe zawierające informacje na temat back-endowej części projektu.

#Stawianie bazy danych (wymagane jest posiadanie zainstalowanej bazy SQL)
- Przed uruchomieniem projektu w Visual Studio przechodzimy do zakłdki Tools -> NugetPackageManager -> PackageManagerConsole.
- Następnie pojawi nam się konsola w którą wpisujuemy "Add-Migration PixBlockMigration -Context PixBlocksContext".
- (Wersja linux: "dotnet ef migrations add PixBlockMigration --context PixBlocksContext")
- Następnie wpisujemy "Add-Migration PixBlockMigration -Context RefreshTokenContext".
- (Wersja linux: "dotnet ef migrations add PixBlockMigration --context RefreshTokenContext")
- Następnie wpisujemy "Update-Database -Context PixBlocksContext" oraz klikamy enter.
- (Wersja linux: "dotnet ef database update --context PixBlocksContext")
- Następnie wpisujemy "Update-Database -Context RefreshTokenContext" oraz klikamy enter.
- (Wersja linux: "dotnet ef database update --context RefreshTokenContext")

#Swagger
- SwaggerUI znajduje się pod adresem: "http://localhost:port/swagger".
- SwaggerJson znajduje się pod adresem: "http://localhost:port/swagger/v1/swagger.json".
