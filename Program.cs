using Microsoft.EntityFrameworkCore;
using api_pertama.Data;
using api_pertama.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")
    ));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapUserEndpoints();
app.MapHobbyEndpoints();
app.MapWorkExperienceEndpoints();

app.Run();