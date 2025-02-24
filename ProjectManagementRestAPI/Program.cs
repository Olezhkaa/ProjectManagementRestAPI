
using Microsoft.EntityFrameworkCore;
using ProjectManagementRestAPI.DataBase;
using ProjectManagementRestAPI.Repositories;
using ProjectManagementRestAPI.Services;
using System;

namespace ProjectManagementRestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ����������� � ��
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

            builder.Services.AddScoped<ProjectRepository>();
            builder.Services.AddScoped<ProjectService>();

            builder.Services.AddScoped<UsersRepository>();
            builder.Services.AddScoped<UsersService>();

            builder.Services.AddScoped<TaskRepository>();
            builder.Services.AddScoped<TaskService>();

            builder.Services.AddScoped<CommentRepository>();
            builder.Services.AddScoped<CommentService>();

            // ���������� ������������
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // �������� Swagger ��� ������������ API
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // �������� ������������� API
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
