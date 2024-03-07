using AzureFunctionCSharpCrud.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzureFunctionCSharpCrud.DbContexts;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<TodoEntity> Todos { get; set; }
}