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
            _context.Activities.Add(activity);
            _context.SaveChanges();
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

        public IEnumerable<Activity> GetCompletedActivity()
        {
            return _context.Activities.Where(x => x.IsComplete).ToList();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public void UpdateActivity(int? id, Activity activity)
        {
            if(id != null)
            _context.Activities.Update(activity);
            _context.SaveChanges();
        }
    }
}
