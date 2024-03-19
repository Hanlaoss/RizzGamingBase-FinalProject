﻿
using Microsoft.EntityFrameworkCore;
using RGB.Back.Models;
using RGB.Back.Repo;
using RGB.Back.Repos;
using RGB.Back.Repos;


namespace RGB.Back
{
	public class Program
	{
		public static void Main(string[] args)
		{
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<RizzContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Rizz"));
            });

            string CorsPolicy = "AllowAny";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: CorsPolicy,
                    policy =>
                    {
                        policy.WithOrigins("*").WithHeaders("*").WithMethods("*");
                    });
            });

            // 注册 RGB.Back.Repos.CartRepository 服务
            builder.Services.AddScoped<CartRepository>();
            builder.Services.AddScoped<GameRepository>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(CorsPolicy);
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
