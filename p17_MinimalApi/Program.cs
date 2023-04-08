using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using p17_MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//todo - add unit tests, error handling
//todo https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-7.0&tabs=visual-studio#prevent-over-posting
var todoitems = app.MapGroup("/todoitems");

// we can inject in lambda params in minimum api
todoitems.MapGet("/todoitems", GetAllTodos);
todoitems.MapGet("/todoitems/completed", GetCompletedTodos);
todoitems.MapGet("/todoitems/{id}", GetTodoById);
todoitems.MapPost("/todoitems", CreateTodo);todoitems.MapPut("/todoitems/{id}", UpdateTodo);
todoitems.MapDelete("/todoitems/{id}", DeleteTodo);

app.Run();

static async Task<IResult> GetAllTodos(TodoDb db) => 
    TypedResults.Ok(await db.Todos.ToArrayAsync());

static async Task<IResult> GetCompletedTodos(TodoDb db) => 
    TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).ToArrayAsync());

static async Task<IResult> GetTodoById(int id, TodoDb db) =>
    await db.Todos.FindAsync(id) is Todo todo
        ? TypedResults.Ok(todo)
        : TypedResults.NotFound();
        
static async Task<IResult> CreateTodo(Todo todo, TodoDb db)
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/todoitems/{todo.Id}", todo);
}

async Task<IResult> UpdateTodo(int id, Todo inputTodo, TodoDb db)
{
    var existingTodo = await db.Todos.FindAsync(id);
    if (existingTodo is null) return TypedResults.NotFound();

    existingTodo.Name = inputTodo.Name;
    existingTodo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodo(int id, TodoDb db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.Ok(todo);
    }

    return TypedResults.NotFound();
}