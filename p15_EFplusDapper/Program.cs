using Microsoft.EntityFrameworkCore;
using p15_EFplusDapper.Infrastructure;
using p15_EFplusDapper.Infrastructure.Data;
using p15_EFplusDapper.Module;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBaseApiModule(builder.Configuration);

builder.Services.AddTransient<ContextInitializer>();
//todo - move connection string to app.settings
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Filename=testDb"));

builder.Services.AddTransient<IUserRepository, UserRepository>(_ => new UserRepository("Filename=testDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseBaseApiModule(builder.Configuration, app.Environment);

if (app.Environment.IsDevelopment())
{
    app.Seed();
}

app.Run();