using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
    public class UserRepository : InterfaceUserRepository
    {
        private readonly TasksDBContex _dbContext;
        public UserRepository(TasksDBContex tasksDBContex) 
        {
            _dbContext = tasksDBContex;
        }
        public async Task<List<UserModel>> FindAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> FindById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Create(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel ExistUser = await FindById(id);

            if(ExistUser == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            ExistUser.Name = user.Name;
            ExistUser.Email = user.Email;

            _dbContext.Users.Update(ExistUser);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel user = await FindById(id);

            if( user == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
