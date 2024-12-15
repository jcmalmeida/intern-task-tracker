using Xunit;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Tests.Data;

public class TodoDbTests
{
    private TodoDb createTodoDb()
    {
        var options = new DbContextOptionsBuilder<TodoDb>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new TodoDb(options);
        return context;
    }

    [Fact]
    public async Task Get_Specified_Todo_Item()
    {
        // Arrange
        using var context = createTodoDb();
        context.Todos.Add(new Todo { Id = 1, Name = "Checar PRs em revisão", isComplete = false });
        context.Todos.Add(new Todo { Id = 2, Name = "Atualizar tasks no Azure", isComplete = true });
        await context.SaveChangesAsync();

        // Act
        var result = await context.Todos.FindAsync(2);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("Atualizar tasks no Azure", result.Name);
        Assert.True(result.isComplete);
    }

    [Fact]
    public async Task Get_All_Todo_Items()
    {
        // Arrange
        using var context = createTodoDb();
        context.Todos.Add(new Todo { Id = 1, Name = "Checar PRs em revisão", isComplete = false });
        context.Todos.Add(new Todo { Id = 2, Name = "Atualizar tasks no Azure", isComplete = true });
        await context.SaveChangesAsync();

        // Act
        var result = await context.Todos.ToListAsync();
        
        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task Update_Specified_Todo_Item()
    {
        // Arrange
        using var context = createTodoDb();
        context.Todos.Add(new Todo { Id = 1, Name = "Checar PRs em revisão", isComplete = false });
        context.Todos.Add(new Todo { Id = 2, Name = "Atualizar tasks no Trello", isComplete = true });
        await context.SaveChangesAsync();

        // Act
        var todoToUpdate = await context.Todos.FindAsync(2);
        if (todoToUpdate != null)
        {
            todoToUpdate.isComplete = false;
            todoToUpdate.Name = "Atualizar tasks no Azure";
            context.Todos.Update(todoToUpdate);
            await context.SaveChangesAsync();
        }
        var result = await context.Todos.FindAsync(2);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("Atualizar tasks no Azure", result.Name);
        Assert.False(result.isComplete);
    }

    [Fact]
    public async Task Remove_Specified_Todo_Item()
    {
        // Arrange
        using var context = createTodoDb();
        context.Todos.Add(new Todo { Id = 1, Name = "Checar PRs em revisão", isComplete = true });
        context.Todos.Add(new Todo { Id = 2, Name = "Atualizar tasks no Azure", isComplete = true });
        await context.SaveChangesAsync();

        // Act
        var todoToRemove = await context.Todos.FindAsync(2);
        if (todoToRemove != null)
        {
            context.Todos.Remove(todoToRemove);
            await context.SaveChangesAsync();
        }
        var result = await context.Todos.ToListAsync();
        
        // Assert
        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Checar PRs em revisão", result[0].Name);
        Assert.True(result[0].isComplete);
    }
}