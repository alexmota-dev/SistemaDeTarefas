﻿using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositories.Interfaces
{
    public interface InterfaceUserRepository
    {
        Task<List<UserModel>> FindAll();
        Task<UserModel> FindById(int id);
        Task<UserModel> Create(UserModel user);
        Task<UserModel> Update(UserModel user, int id);
        Task<bool> Delete(int id);
    }
}
