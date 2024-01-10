using Lab4.DB;
using Lab4.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lab4.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly LR4Context _context;

        public TaskRepository(LR4Context context)
        {
            _context = context;
        }

        public async Task AddTask(MyTask task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                throw; // Перебросьте исключение, чтобы оно могло быть обработано на уровне выше
            }
        }

        public async Task<List<MyTask>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<MyTask> GetTopPriorityTask()
        {
            return await _context.Tasks
                .Where(task => !task.IsDone)
                .OrderByDescending(task => task.Priority)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MyTask>> FindTasksByPriority(int priority)
        {
            return await _context.Tasks
                .Where(task => task.Priority == priority && !task.IsDone)
                .ToListAsync();
        }

        public async Task<MyTask> GetTaskByName(string taskName)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(task => task.Name == taskName);
        }

        public async Task RemoveTask(MyTask task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}