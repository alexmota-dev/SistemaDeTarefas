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

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> FindById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id inválido");
            }
            TaskModel task = await _taskRepository.FindById(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Create([FromBody] TaskModel taskModel)
        {
            if (taskModel == null)
            {
                return BadRequest("Body Inválido");
            }
            TaskModel task = await _taskRepository.Create(taskModel);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Update([FromBody] TaskModel taskModel, int id)
        {
            if(taskModel == null || id < 1)
            {
                return BadRequest("Requisição invalida, verifique o id e o body enviados.");
            }

            taskModel.Id = id;
            TaskModel task = await _taskRepository.Update(taskModel, id);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> Delete(int id)
        {
            bool taskDeleted = await _taskRepository.Delete(id);
            if (taskDeleted)
            {
                return Ok("Task Deletada");
            }
            else
            {
                return Ok("Task não existe no banco");
            }
        }


    }
}
