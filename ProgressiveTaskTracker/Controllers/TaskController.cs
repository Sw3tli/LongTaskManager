using Microsoft.AspNetCore.Mvc;
using ProgressiveTaskTracker.Services.Interfaces;

namespace IncrementalTaskMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        [HttpPost("starttask")]
        public async Task<IActionResult> StartTask()
        {
            try
            {
                string result = await _taskService.StartTaskAsync();
                return Ok(result); // Return success message or task ID
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("taskprogress")]
        public async Task<IActionResult> GetTaskProgress()
        {
            try
            {
                string progress = await _taskService.GetTaskProgressAsync();
                return Ok(progress); // Return task progress
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}