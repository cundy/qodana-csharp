using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureFunctionCSharpCrud.Migrations
{
  /// <inheritdoc />
  public partial class namestoredProc : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      var dropProcSql = @"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE SPECIFIC_SCHEMA = N'dbo' AND SPECIFIC_NAME = N'StoredProcedureName' AND ROUTINE_TYPE = N'PROCEDURE')
            DROP PROCEDURE dbo.StoredProcedureName";
      migrationBuilder.Sql(dropProcSql);
      var createProcSql = @"CREATE PROCEDURE [dbo].[StoredProcedureName] @completed int = 0 AS 
          BEGIN
            SELECT * from dbo.Todo as todo where todo.completed = @completed
          END";
      migrationBuilder.Sql(createProcSql);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      var dropProcSql = "DROP PROC StoredProcedureName";
      migrationBuilder.Sql(dropProcSql);
    }
  }
}
