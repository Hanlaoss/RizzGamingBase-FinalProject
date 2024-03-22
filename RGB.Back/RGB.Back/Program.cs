
using Microsoft.EntityFrameworkCore;
using RGB.Back.Models;
using RGB.Back.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace RGB.Back
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			//jwt

			builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));


			var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);

			TokenValidationParameters tokenValidationParams = new TokenValidationParameters
			{
				RequireExpirationTime = false,
				ValidateIssuer = false,
				ValidateAudience = false,

				//����IssuerSigningKey
				ValidateIssuerSigningKey = true,
				//�HJwtConfig:Secret��Key�A����Jwt�[�K
				IssuerSigningKey = new SymmetricSecurityKey(key),

				//���Үɮ�
				ValidateLifetime = true,

				//�]�wtoken���L���ɶ��i�H�H��ӭp��A��token���L���ɶ��C�󤭤����ɨϥΡC
				ClockSkew = TimeSpan.Zero
			};

			//���UtokenValidationParams�A����i�H�`�J�ϥΡC
			builder.Services.AddSingleton(tokenValidationParams);

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(jwt =>
			{
				jwt.SaveToken = true;
				jwt.TokenValidationParameters = tokenValidationParams;
			});
			//�H�Wjwt
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

			app.UseCors();
			app.UseHttpsRedirection();
			//�ҥΨ����ѧO
			app.UseAuthentication();
			//�ҥα��v�\��
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}

	}
}
