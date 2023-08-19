using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementSystemProject;
using TaskManagementSystemProject.Context;
using TaskManagementSystemProject.Model;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = taskItem.Id }, taskItem);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem taskItem)
        {
            TaskItem existingTask = await _context.TaskItems.FindAsync(id);

            if(existingTask == null)
            {
                return NotFound();
            }
            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.Status = taskItem.Status;
            existingTask.DueDate = taskItem.DueDate;
            existingTask.Priority = taskItem.Priority;

            _context.Update(existingTask);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }

        [HttpPost("Assign/{taskId}")]
        public async Task<IActionResult> AssignTask(int taskId, [FromBody] string assignId)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskId);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.AssignId = assignId;
            await _context.SaveChangesAsync();

            return Ok("Task assigned successfully");
        }

        [HttpPost("AddComment/{taskId}")]
        public async Task<IActionResult> AddComment(int taskId, [FromBody] TaskComment comment)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskId);
            if (taskItem == null)
            {
                return NotFound();
            }
            var taskComment = new TaskComment
            {
                TaskId = comment.TaskId,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Text = comment.Text,
            };


            _context.TaskComments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok("Comment added successfully");
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchTasks(string keyword)
        {
            // Perform a database query to search for tasks based on the keyword.
            var tasks = await _context.TaskItems
                .Where(task => task.Title.Contains(keyword) || task.Description.Contains(keyword))
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("SortByPriority")]
        public async Task<IActionResult> SortTasksByPriority()
        {
            var tasks = await _context.TaskItems
                .OrderBy(task => task.Priority)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("SortByDueDate")]
        public async Task<IActionResult> SortTasksByDueDate()
        {
            var tasks = await _context.TaskItems
                .OrderBy(task => task.DueDate)
                .ToListAsync();

            return Ok(tasks);
        }

    }
}
