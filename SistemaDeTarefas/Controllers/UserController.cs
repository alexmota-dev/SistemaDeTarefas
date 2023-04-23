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
    public class UserController : ControllerBase
    {
        private readonly InterfaceUserRepository _userRepository;
        public UserController(InterfaceUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<List<UserModel>>> FindAll()
        {
            List<UserModel> users = await _userRepository.FindAll();
            return Ok(users);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> FindById(int id)
        {
            //inicialmente a regra de negocio vai ficar aqui no controller, depois eu passo ela para um service e faço a injeção de dependencia.
            if(id < 1)
            {
                return BadRequest("Id Inválido");
            }
            UserModel user = await _userRepository.FindById(id);
            return Ok(user);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<UserModel>> Create([Microsoft.AspNetCore.Mvc.FromBody] UserModel userModel)
        {
            UserModel user = await _userRepository.Create(userModel);
            return Ok(user);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([System.Web.Http.FromBody] UserModel userModel, int id)
        {
            if(userModel == null || id < 1)
            {
                return BadRequest("Requisição inválida, verifique o id e o body enviados.");
            }
            userModel.Id = id;
            UserModel user = await _userRepository.Update(userModel, id);
            return Ok(user);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            bool userDeleted = await _userRepository.Delete(id);
            if(userDeleted)
            {
                return Ok(userDeleted);
            }
            else
            {
                return Ok("User não existe no banco");
            }
        }
    }
}
