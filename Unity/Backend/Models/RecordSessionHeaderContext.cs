using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class RecordSessionHeaderContext : DbContext
{
    public RecordSessionHeaderContext(DbContextOptions<RecordSessionHeaderContext> options)
        : base(options)
    {
    }

    public DbSet<RecordSessionHeader> RecordSessionHeaders { get; set; } = null!;
}