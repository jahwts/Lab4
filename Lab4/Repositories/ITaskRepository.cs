using Lab4.Models;
using System.Threading.Tasks;

namespace Lab4.Repositories
{
    public interface ITaskRepository
    {
        Task AddTask(MyTask task);

        Task<List<MyTask>> GetAllTasks();

        Task<MyTask> GetTopPriorityTask();

        Task<List<MyTask>> FindTasksByPriority(int priority);

        Task<MyTask> GetTaskByName(string taskName);

        Task RemoveTask(MyTask task);

    }
}