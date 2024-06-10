using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Services;
using ToDoList.Interface;
using ToDoList.Services.Interfaces;
using ToDoList.Class.DTO;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IUserRepository _usersRepository;

        public TasksController(ITasksRepository tasksRepository, IUserRepository usersRepository)
        {
            _tasksRepository = tasksRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet("GetMy"), Authorize]
        public async Task<IActionResult> GetMy()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
                return Unauthorized("User has to be logged");
            var tasks = await _tasksRepository.GetTasksByUserId(userId);

            return Ok(tasks);
        }

        [HttpGet("GetFamily"), Authorize]
        public async Task<IActionResult> GetFamily()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
                return Unauthorized("User has to be logged");
            var tasks = await _tasksRepository.GetFamilyTasks(userId);

            return Ok(tasks);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Add([FromBody] TaskToDoDTO taskToDoDTO)
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null)
                return Unauthorized("User has to be logged");

            var user = await _usersRepository.GetUserById(userId);
            await _tasksRepository.AddTask(user, taskToDoDTO);

            return Ok("the task has been added");
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _tasksRepository.DeleteTask(id);
            return Ok("the task has been deleted");
        }

        [HttpPatch, Authorize]
        public async Task<IActionResult> Update([FromBody] TaskToPatchDTO taskToDo)
        {
            var task = await _tasksRepository.GetByTaskId(taskToDo.Id);
            if (task == null)
                return BadRequest("the task has not been found");

            task.Description = taskToDo.Description;
            task.IsCompleted = taskToDo.IsCompleted;
            await _tasksRepository.UpdateTask(task);
            return Ok("the task has been updated");
        }
    }
}