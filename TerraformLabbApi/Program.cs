using Microsoft.EntityFrameworkCore;
using TerraformLabbApi.Data;
using TerraformLabbApi.Models;

namespace TerraformLabbApi
{
	public class Program
	{
		//public static ApplicationDbContext Context = new ApplicationDbContext();
		public static void Main(string[] args)
		{
            //string connectionString = "Server=localhost,1433;Database=ProductDB123;User Id=sa;Password=Password12345!;Trust Server Certificate=true";
            string connectionString = "Server=sqlserver;Database=ProductDB123;User Id=sa;Password=Password12345!;Trust Server Certificate=true";
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
			

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();
			
			using(var scope = app.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				context.Database.Migrate();

				if (!context.Characters.Any())
				{
					context.Characters.AddRange
						(
						new Character { Name = "Yin", Age = 12, IsMale = false },
						new Character { Name = "Yang", Age = 12, IsMale = true },
						new Character { Name = "Yo", Age = 60, IsMale = true }
						);
					context.SaveChanges();
				}
			}


			//Endpoints
			app.MapGet("/api/characters", async (ApplicationDbContext db) => await db.Characters.ToListAsync());

			/*
            app.MapPost("/api/character", async (Character character) =>
            {
                var repository = new CharacterRepository(Context);
                repository.AddCharacterAsync(character);

                return Results.Ok(character.Name + " was successfuly added to database");
            });
			*/

			app.Run();
		}
	}
}
