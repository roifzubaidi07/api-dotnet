using Microsoft.EntityFrameworkCore;
using api_pertama.Data;
using api_pertama.Models;
using api_pertama.Dtos;

namespace api_pertama.Endpoints;

public static class WorkExperienceEndpoints
{
    public static void MapWorkExperienceEndpoints(this WebApplication app)
    {
        app.MapPost("/users/{userId}/works", async (
            int userId,
            WorkExperience work,
            AppDbContext db) =>
        {
            var userExists = await db.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                return Results.NotFound("User not found");

            work.UserId = userId;
            db.WorkExperiences.Add(work);
            await db.SaveChangesAsync();

            return Results.Ok(work);
        });

        app.MapPut("/users/{userId}/works/{id}", async (
            int userId,
            int id,
            UpdateWorkExperienceDto dto,
            AppDbContext db) =>
        {
            var work = await db.WorkExperiences.FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);
            if (work is null)
                return Results.NotFound();

            work.Company = dto.Company;
            work.Position = dto.Position;
            work.Years = dto.Years;

            await db.SaveChangesAsync();
            return Results.Ok(work);
        });
    }
}