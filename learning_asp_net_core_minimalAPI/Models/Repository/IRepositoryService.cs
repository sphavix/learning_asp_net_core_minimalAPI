namespace learning_asp_net_core_minimalAPI.Models.Repository
{
    public interface IRepositoryService
    {
        IEnumerable<Activity> GetAllActivities();
        Activity GetActivity(int id);
        IEnumerable<Activity> GetCompletedActivity();
        void AddActivity(Activity activity);
        void UpdateActivity(int id, Activity activity);
        void DeleteActivity(int id);
    }
}
