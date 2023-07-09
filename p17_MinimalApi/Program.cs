using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using p17_MinimalApi;

internal class Program
{
    public static IConfiguration Config { get; private set; }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
        builder.Services.AddScoped<ICacheService, CacheService>();

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
        todoitems.MapPost("/todoitems", (Todo todo, TodoDb db, ICacheService cacheService) => CreateTodo(todo, db, cacheService));todoitems.MapPut("/todoitems/{id}", UpdateTodo);
        todoitems.MapDelete("/todoitems/{id}", DeleteTodo);

        Config = app.Configuration;
        app.Run();

        static async Task<IResult> GetAllTodos(TodoDb db) => 
            TypedResults.Ok(await db.Todos.ToArrayAsync());

        static async Task<IResult> GetCompletedTodos(TodoDb db) => 
            TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).ToArrayAsync());

        static async Task<IResult> GetTodoById(int id, TodoDb db, ICacheService cache)
        {
            var cachedValue = await cache.GetValueAsync<Todo>(id.ToString());

            if (cachedValue != null)
                return TypedResults.Ok(cachedValue);
    
            if (await db.Todos.FindAsync(id) is Todo todo)
            {
                await cache.SetValueAsync(id.ToString(), todo);
                return TypedResults.Ok(todo);
            }

            return TypedResults.NotFound();
        }

        static async Task<IResult> CreateTodo(Todo todo, TodoDb db, ICacheService cache)
        {
            db.Todos.Add(todo);
            await db.SaveChangesAsync();

            await cache.SetValueAsync(todo.Id.ToString(), todo);

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
    }
}