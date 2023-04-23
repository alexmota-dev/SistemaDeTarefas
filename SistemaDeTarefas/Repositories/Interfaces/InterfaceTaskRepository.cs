using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositories.Interfaces
{
    public interface InterfaceTaskRepository
    {
        Task<List<TaskModel>> FindAll();
        Task<TaskModel> FindById(int id);
        Task<TaskModel> Create(TaskModel Task);
        Task<TaskModel> Update(TaskModel Task, int id);
        Task<bool> Delete(int id);
    }
}
