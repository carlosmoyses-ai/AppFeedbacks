using Feedback.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Api.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {     
    }

    public DbSet<FeedbackModel> Feedbacks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Feedbacks");
    }
}