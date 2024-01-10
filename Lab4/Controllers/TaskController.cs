using Microsoft.AspNetCore.Mvc;
using Lab4.Services;
using System;
using System.Threading.Tasks;
using Lab4.Models;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequest taskRequest)
        {
            try
            {
                await _taskService.CreateTask(taskRequest);
                return Ok("Task created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating task: {ex.Message}");
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskService.GetAllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting tasks: {ex.Message}");
            }
        }

        [HttpGet("get-top-priority")]
        public async Task<IActionResult> GetTopPriorityTask()
        {
            try
            {
                var topPriorityTask = await _taskService.GetTopPriorityTask();

                if (topPriorityTask != null)
                {
                    return Ok(topPriorityTask);
                }
                else
                {
                    return NotFound("No tasks found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting top priority task: {ex.Message}");
            }
        }

        [HttpGet("find-by-priority/{priority}")]
        public async Task<ActionResult<List<MyTask>>> FindTasksByPriority(int priority)
        {
            try
            {
                var tasks = await _taskService.FindTasksByPriority(priority);

                if (tasks != null && tasks.Any())
                {
                    return Ok(tasks);
                }
                else
                {
                    return NotFound($"No tasks found with priority {priority}.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error finding tasks by priority: {ex.Message}");
            }
        }

        [HttpDelete("remove/{taskName}")]
        public async Task<ActionResult> RemoveTask(string taskName)
        {
            try
            {
                var success = await _taskService.RemoveTask(taskName);

                if (success)
                {
                    return Ok($"Task '{taskName}' removed successfully.");
                }
                else
                {
                    return NotFound($"Task '{taskName}' not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error removing task: {ex.Message}");
            }
        }

        [HttpPost("save-to-json/{filePath}")]
        public async Task<ActionResult> SaveTasksToJson(string filePath)
        {
            try
            {
                var success = await _taskService.SaveTasksToJson(filePath);

                if (success)
                {
                    return Ok("Tasks saved to JSON successfully.");
                }
                else
                {
                    return BadRequest("Error saving tasks to JSON.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error saving tasks to JSON: {ex.Message}");
            }
        }
    }
}