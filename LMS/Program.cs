
using LMS.Interface;
using LMS.Model;
using LMS.Repo;
using Microsoft.EntityFrameworkCore;

namespace LMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // builder.Services.AddControllers();
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
                   options.SuppressModelStateInvalidFilter = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string? connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string is missing! Set the DB_CONNECTION_STRING environment variable.");
            }

            builder.Services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });


            builder.Services.AddScoped<Icourses, coursesRepo>();
            builder.Services.AddScoped<Ilectures, lecturesRepo>();
            builder.Services.AddScoped<Icategories, categoriesRepo>();
            builder.Services.AddScoped<Imaterial, materialRepo>();
            builder.Services.AddScoped<Istudents, studentsRepo>();
            builder.Services.AddScoped<GeneralRes>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
