using Microsoft.EntityFrameworkCore;
using api_pertama.Data;
using api_pertama.Models;
using api_pertama.Dtos;

namespace api_pertama.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", async (AppDbContext db) =>
            await db.Users
                .Include(u => u.Hobbies)
                .Include(u => u.WorkExperiences)
                .ToListAsync()
        );
        
        app.MapPost("/users", async (CreateUserDto dto, AppDbContext db) =>
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Results.Created($"/users/{user.Id}", user);
        });

        app.MapGet("/users/{id}", async (int id, AppDbContext db) =>
        {
            var user = await db.Users
                .Include(u => u.Hobbies)
                .Include(u => u.WorkExperiences)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user is null
                ? Results.NotFound()
                : Results.Ok(user);
        });

        app.MapPut("/users/{id}", async (int id, UpdateUserDto dto, AppDbContext db) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                return Results.NotFound();

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Age = dto.Age;

            await db.SaveChangesAsync();
            return Results.Ok(user);
        });
    }
}