using Lab4.Models;
using Lab4.Repositories;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab4.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task CreateTask(TaskRequest taskRequest)
        {
            var task = new MyTask
            {
                Name = taskRequest.Name,
                Priority = taskRequest.Priority,
                Deadline = taskRequest.Deadline,
                IsDone = false
            };

            await _taskRepository.AddTask(task);
        }

        public async Task<List<MyTask>> GetAllTasks()
        {
            return await _taskRepository.GetAllTasks();
        }

        public async Task<MyTask> GetTopPriorityTask()
        {
            var topPriorityTask = await _taskRepository.GetTopPriorityTask();

            if (topPriorityTask != null)
            {
                return new MyTask
                {
                    Name = topPriorityTask.Name,
                    Priority = topPriorityTask.Priority,
                    Deadline = topPriorityTask.Deadline,
                    IsDone = topPriorityTask.IsDone
                };
            }

            return null;
        }

        public async Task<List<MyTask>> FindTasksByPriority(int priority)
        {
            return await _taskRepository.FindTasksByPriority(priority);
        }

        public async Task<bool> RemoveTask(string taskName)
        {
            var taskToRemove = await _taskRepository.GetTaskByName(taskName);

            if (taskToRemove != null)
            {
                await _taskRepository.RemoveTask(taskToRemove);
                return true;
            }

            return false;
        }

        public async Task<bool> SaveTasksToJson(string filePath)
        {
            try
            {
                var tasks = await _taskRepository.GetAllTasks();
                var json = JsonSerializer.Serialize(tasks);
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}