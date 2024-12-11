using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiCLI;

public static class TodoManager
{
    public static string todoEndpoint = "/todoitems";
    public static JsonSerializerOptions options =
        new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

    public static async Task<bool> GetTodoExist(HttpClient client, int todoId)
    {
        var endpoint = todoEndpoint + "/" + todoId;
        var response = await client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
            return true;

        return false;
    }

    public static async Task AddTodo(HttpClient client, string description)
    {
        var todo = new
        {
            id = 0,
            name = description,
            isComplete = false
        };

        try
        {
            var response = await client.PostAsJsonAsync(todoEndpoint, todo);

            if (response.IsSuccessStatusCode)
                return;
        }
        catch (Exception)
        {}

        throw new HttpRequestException("Erro na operação");
    }

    public static async Task DeleteTodo(HttpClient client, int todoId)
    {
        var endpoint = todoEndpoint + "/" + todoId;
        
        var todoExist = await GetTodoExist(client, todoId);

        if (!todoExist)
            throw new ArgumentException("ID inválido");
        
        try
        {
            var response = await client.DeleteAsync(endpoint);

            if (response.IsSuccessStatusCode)
                return;
        }
        catch (Exception)
        {}

        throw new HttpRequestException("Erro na operação");
    }

    public static async Task MarkTaskAsCompleted(HttpClient client, int todoId)
    {
        var endpoint = todoEndpoint + "/" + todoId;
        
        var todoExist = await GetTodoExist(client, todoId);

        if (!todoExist)
            throw new ArgumentException("ID inválido");

        var currentTodo = await GetTodo(client, todoId);

        var updatedTodo = new
        {
            id = 0,
            name = currentTodo?.Name,
            isComplete = true
        };

        try
        {
            var response = await client.PutAsJsonAsync(endpoint, updatedTodo);

            if (response.IsSuccessStatusCode)
                return;
        }
        catch (Exception)
        {}

        throw new HttpRequestException("Erro na operação");
    }

    public static async Task UpdateTask(HttpClient client, int todoId, string newName)
    {
        var endpoint = todoEndpoint + "/" + todoId;
        
        var todoExist = await GetTodoExist(client, todoId);

        if (!todoExist)
            throw new ArgumentException("ID inválido");

        var currentTodo = await GetTodo(client, todoId);

        var updatedTodo = new
        {
            id = currentTodo?.Id,
            name = newName,
            isComplete = currentTodo?.isComplete
        };

        try
        {
            var response = await client.PutAsJsonAsync(endpoint, updatedTodo);

            if (response.IsSuccessStatusCode)
                return;
        }
        catch (Exception)
        {}

        throw new HttpRequestException("Erro na operação");
    }

    public static async Task<Todo?> GetTodo(HttpClient client, int todoId)
    {
        var endpoint = todoEndpoint + "/" + todoId;

        try
        {
            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonSerializer.Deserialize<Todo>(content, options);
                return todo;
            }
        }
        catch (Exception)
        {}

        throw new HttpRequestException("Erro na operação");
    }

    public static async Task<List<Todo>> GetAllTodos(HttpClient client)
    {
        try
        {
            var response = await client.GetAsync(todoEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todos = JsonSerializer.Deserialize<List<Todo>>(content, options);
                return todos ?? new List<Todo>();
            }
        }
        catch (Exception)
        {}

        throw new HttpRequestException("Erro na operação");
    }
}