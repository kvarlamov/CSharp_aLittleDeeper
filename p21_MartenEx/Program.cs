using Marten;
using Microsoft.EntityFrameworkCore;
using p21_MartenEx.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("MartenDB");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMarten(opt =>
{
    opt.Connection(connectionString);
});

builder.Services.AddTransient<IRepository, Repository>();

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

app.Run();

/*
docker run --name myPostgresDb -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=88888888 -e POSTGRES_DB=MartenEx -d postgres
*/