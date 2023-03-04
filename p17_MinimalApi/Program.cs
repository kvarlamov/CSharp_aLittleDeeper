using Microsoft.EntityFrameworkCore;
using p17_MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var todoitems = app.MapGroup("/todoitems");

// we can inject in lambda params in minimum api
todoitems.MapGet("/todoitems", async (TodoDb db) => await db.Todos.ToListAsync());
todoitems.MapGet("/todoitems/completed", async (TodoDb db) => await db.Todos.Where(t => t.IsComplete).ToListAsync());
todoitems.MapGet("/todoitems/{id}", async (int id, TodoDb db) => await db.Todos.FindAsync(id)
    is Todo todo
    ? Results.Ok(todo)
    : Results.NotFound());

todoitems.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    
    return Results.Created($"/todoitems/{todo.Id}", todo);
});

todoitems.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var existingTodo = await db.Todos.FindAsync(id);
    if (existingTodo is null)
        return Results.NotFound();

    existingTodo.Name = inputTodo.Name;
    existingTodo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

todoitems.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();

        return Results.Ok(todo);
    }

    return Results.NotFound();
});

app.Run();