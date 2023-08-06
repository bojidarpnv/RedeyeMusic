using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;

namespace RedeyeMusic.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<RedeyeMusicDbContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddScoped<ISongService, SongService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("RedeyeMusicSystem", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:7150")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
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

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("RedeyeMusicSystem");
            app.Run();
        }
    }
}