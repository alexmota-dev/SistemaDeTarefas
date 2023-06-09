using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Repositories;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TasksDBContex>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            /*builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<TasksDBContex>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );*/

            //configurando inje��o de dependencia - user
            builder.Services.AddScoped<InterfaceUserRepository, UserRepository>();
            //configurando inje��o de dependencia - task
            builder.Services.AddScoped<InterfaceTaskRepository, TaskRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}