namespace learning_asp_net_core_minimalAPI.Models.Repository
{
    public interface IRepositoryService
    {
        List<Activity> GetAllActivities();
        Activity GetActivity(int id);
        List<Activity> GetCompletedActivity();
        void AddActivity(Activity activity);
        void UpdateActivity(Activity activity);
        void DeleteActivity(int id);
    }
}
