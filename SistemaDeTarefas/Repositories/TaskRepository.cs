using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
    public class TaskRepository : InterfaceTaskRepository
    {
        private readonly TasksDBContex _dbContext;
        public TaskRepository(TasksDBContex tasksDBContex) 
        {
            _dbContext = tasksDBContex;
        }
        public async Task<List<TaskModel>> FindAll()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskModel> FindById(int id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskModel> Create(TaskModel Task)
        {
            await _dbContext.Tasks.AddAsync(Task);
            await _dbContext.SaveChangesAsync();
            return Task;
        }

        public async Task<TaskModel> Update(TaskModel Task, int id)
        {
            TaskModel ExistTask = await FindById(id) ?? throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            ExistTask.Name = Task.Name;
            ExistTask.Description = Task.Description;
            ExistTask.Status = Task.Status;

            _dbContext.Tasks.Update(ExistTask);
            await _dbContext.SaveChangesAsync();

            return Task;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel Task = await FindById(id) ?? throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");

            _dbContext.Tasks.Remove(Task);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
