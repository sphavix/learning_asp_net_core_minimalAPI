using Microsoft.EntityFrameworkCore;

namespace learning_asp_net_core_minimalAPI.Models.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ActivityDbContext _context;

        public RepositoryService(ActivityDbContext context)
        {
            this._context = context;
        }
        public void AddActivity(Activity activity)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Activities ON;");
                _context.Activities.Add(activity);
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Activities OFF;");
                transaction.Commit();
            }
            
        }

        public void DeleteActivity(int id)
        {
            var activity = _context.Activities.FirstOrDefault(x => x.Id == id);
            if(activity != null)
                _context.Activities.Remove(activity);
            _context.SaveChanges();
        }

        public Activity GetActivity(int id)
        {
            return _context.Activities.FirstOrDefault(x => x.Id == id);
          
        }

        public List<Activity> GetCompletedActivity()
        {
            return _context.Activities.Where(x => x.IsComplete).ToList();
        }

        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public void UpdateActivity(Activity activity)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                var result = _context.Activities.FirstOrDefault(x => x.Id == activity.Id);
                if(result != null)
                {   
                    result.Id = activity.Id;
                    result.Name = activity.Name;
                    result.IsComplete = activity.IsComplete;

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Activities ON;");
                    _context.Update(result);
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Activities OFF;");
                    transaction.Commit();
                }
            }
        }
    }
}
