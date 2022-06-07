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
            _context.Activities.Update(activity);
            _context.SaveChanges();
        }
    }
}
