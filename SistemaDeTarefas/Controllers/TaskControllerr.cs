using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;
using System.Net;
using System.Web.Http;

namespace SistemaDeTarefas.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly InterfaceTaskRepository _taskRepository;
        public TaskController(InterfaceTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<List<TaskModel>>> FindAll()
        {
            List<TaskModel> taks = await _taskRepository.FindAll();
            return Ok(taks);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> FindById(int id)
        {
            //inicialmente a regra de negocio vai ficar aqui no controller, depois eu passo ela para um service e faço a injeção de dependencia.
            if(id < 1)
            {
                return BadRequest("Id Inválido");
            }
            TaskModel task = await _taskRepository.FindById(id);
            return Ok(task);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("byUsers/{id}")]
        public async Task<ActionResult<List<TaskModel>>> FindByUser(int id)
        {
            List<TaskModel> tasks = await _taskRepository.FindByUser(id);
            return tasks;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<TaskModel>> Create([Microsoft.AspNetCore.Mvc.FromBody] TaskModel taskModel)
        {
            TaskModel task = await _taskRepository.Create(taskModel);
            return Ok(task);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Update([System.Web.Http.FromBody] TaskModel taskModel, int id)
        {
            if(taskModel == null || id < 1)
            {
                return BadRequest("Requisição inválida, verifique o id e o body enviados.");
            }
            taskModel.Id = id;
            TaskModel task = await _taskRepository.Update(taskModel, id);
            return Ok(task);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> Delete(int id)
        {
            bool taskDeleted = await _taskRepository.Delete(id);
            if(taskDeleted)
            {
                return Ok(taskDeleted);
            }
            else
            {
                return Ok("Task não existe no banco");
            }
        }
    }
}
