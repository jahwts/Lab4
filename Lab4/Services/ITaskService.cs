using Lab4.Models;
using System;
using System.Threading.Tasks;

namespace Lab4.Services
{
    public interface ITaskService
    {
        Task CreateTask(TaskRequest taskRequest);

        Task<List<MyTask>> GetAllTasks();

        Task<MyTask> GetTopPriorityTask();

        Task<List<MyTask>> FindTasksByPriority(int priority);

        Task<bool> RemoveTask(string taskName);

        Task<bool> SaveTasksToJson(string filePath);
    }

    public class TaskRequest
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime Deadline { get; set; }
    }
}