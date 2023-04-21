using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositories.Interfaces
{
    public interface InterfaceUserRepository
    {
        Task<List<UserModel>> FindAllUsers();
        Task<UserModel> FindById(int id);
        Task<UserModel> Create(UserModel user);
        Task<UserModel> Update(UserModel user, int id);
        Task Delete(int id);
    }
}
