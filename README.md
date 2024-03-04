# C# documentation

https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/
VsCode dotnet introduction
https://code.visualstudio.com/docs/csharp/introvideos-csharp

# Entity Framework

https://learn.microsoft.com/en-us/ef/core/

Migrations command:

```
export SQL_CONNECTION_STRING="<connection-string-value>"
// Create migration
dotnet ef migrations add <MigrationName>

// Run migration
dotnet ef database update

// Revert migration
dotnet ef database update  <PreviousMigrationName>
```
