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

        public async Task<TaskModel> Create(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel> Update(TaskModel task, int id)
        {
            TaskModel existTask = await FindById(id) ?? throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            existTask.Name = task.Name;
            existTask.Description = task.Description;
            existTask.Status = task.Status;

            _dbContext.Tasks.Update(existTask);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel Task = await FindById(id) ?? throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");

            _dbContext.Tasks.Remove(Task);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<TaskModel>> FindByUser(int id)
        {
            IQueryable<TaskModel> query = _dbContext.Tasks;
            //eu posso adicionar varias querys diferentes
            query = query.Where(x => x.UserId == id);
            return await query.ToListAsync();
        }
    }
}
