using Microsoft.EntityFrameworkCore;

namespace learning_asp_net_core_minimalAPI.Models
{
    public class ActivityDbContext : DbContext
    {
        public ActivityDbContext(DbContextOptions<ActivityDbContext> options) :
        base(options)
        {

        }

        public DbSet<Activity> Activities => Set<Activity>();
    }
}
