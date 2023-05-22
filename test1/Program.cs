
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using test1.Models;
using test1.Services;

namespace test1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IGenraService, GenraService>();
            builder.Services.AddTransient<IMoviesService,MoviesService>();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
            builder.Services.AddSwaggerGen(options=>
            {
                options.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo 
                {
                    Version= "v1",
                    Title="Mohamed",
                    TermsOfService=new Uri("https://www.facebook.com/profile.php?id=100007371522123")
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}