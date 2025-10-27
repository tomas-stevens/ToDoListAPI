using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data;
using ToDoListAPI.Models;
using SD = System.Drawing;

var builder = WebApplication.CreateBuilder(args);

//Add the DataBase context to the services container.

builder.Services.AddDbContext<ToDoDataBaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoListAPI")));

var app = builder.Build();

// Configure the API endpoints.

// Get all ToDoDataBaseContext items
app.MapGet("/todos", async (ToDoDataBaseContext db) =>
    await db.ToDoLists.ToListAsync());

// Get completed ToDoDataBaseContext items
app.MapGet("/todos/complete", async (ToDoDataBaseContext db) =>
    await db.ToDoLists.Where(t => t.IsCompleted).ToListAsync());

// Get a specific ToDoLists item by ID
app.MapGet("/todos/{id}", async (Guid id, ToDoDataBaseContext db) =>
    await db.ToDoLists.FindAsync(id)
        is ToDoList todo
            ? Results.Ok(todo)
            : Results.NotFound());


// Create a new todo item
app.MapPost("/todos", async (ToDoList todo, ToDoDataBaseContext db) =>
{
    db.ToDoLists.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todos/{todo.Id}", todo);
});


// Update an existing todo item
app.MapPut("/todos/{id}", async (Guid id, ToDoList inputTodo, ToDoDataBaseContext db) =>
{
    var todo = await db.ToDoLists.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Title = inputTodo.Title;
    todo.IsCompleted = inputTodo.IsCompleted;
    todo.Description = inputTodo.Description;
    todo.DueDate = inputTodo.DueDate;
    todo.Priority = inputTodo.Priority;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete a todo item
app.MapDelete("/todos/{id}", async (Guid id, ToDoDataBaseContext db) =>
{
    if (await db.ToDoLists.FindAsync(id) is ToDoList todo)
    {
        db.ToDoLists.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();