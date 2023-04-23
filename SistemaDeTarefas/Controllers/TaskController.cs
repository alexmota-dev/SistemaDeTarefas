using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    public class TaskController : Controller
    {
        private readonly InterfaceTaskRepository _taskRepository;

        public TaskController(InterfaceTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> FindAll()
        {
            List<TaskModel> task = await _taskRepository.FindAll();
            return Ok(task);
        }
    }
}
