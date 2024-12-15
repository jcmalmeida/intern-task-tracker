using TodoApiCLI;
using System.Runtime.InteropServices;

namespace TodoApiCLI;

public static class CliManager
{
    public static string DetectOS()
    {
        var os = "";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            os = "Windows";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            os = "Linux";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            os = "Mac OS";
        else
            os = "Sistema operacional desconhecido";
        return os;
    }

    public static async Task AddTodo(HttpClient client)
    {
        var todoDescription = "";
        do {
            Console.Write("\nDigite a descrição da tarefa (5 - 255 caracteres): ");
            todoDescription = Console.ReadLine();

            if (todoDescription?.Length >= 5 && todoDescription?.Length <= 255)
            {
                try
                {
                    await TodoManager.AddTodo(client, todoDescription);
                    Console.WriteLine("Tarefa adicionada com sucesso!");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro ao adicionar a tarefa. Tente novamente!\n");
                }
            }

            Console.WriteLine("Descrição não respeita o limite de caracteres. Tente novamente!\n");
        }
        while (true);        
    }

    public static async Task DeleteTodo(HttpClient client)
    {
        var todoId = "";
        do
        {
            Console.Write("\nDigite o ID da tarefa que deseja remover: ");
            todoId = Console.ReadLine();

            try
            {
                await TodoManager.DeleteTodo(client, int.Parse(todoId));
                Console.WriteLine("Tarefa apagada com sucesso!");
                break;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Erro ao remover a tarefa. Tente novamente!");
            }
            catch (Exception)
            {
                Console.WriteLine("Não existe tarefa com o ID fornecido. Tente novamente!");
            }
        }
        while (true);
    }

    public static async Task MarkTodoAsCompleted(HttpClient client)
    {
        var todoId = "";
        do
        {
            Console.Write("\nDigite o ID da tarefa que deseja completar: ");
            todoId = Console.ReadLine();

            try
            {
                await TodoManager.MarkTaskAsCompleted(client, int.Parse(todoId));
                Console.WriteLine("Tarefa completada com sucesso!");
                break;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Erro ao completar a tarefa. Tente novamente!");
            }
            catch (Exception)
            {
                Console.WriteLine("Não existe tarefa com o ID fornecido. Tente novamente!\n");
            }
        }
        while (true);
    }

    public static async Task UpdateTodo(HttpClient client)
    {
        var todoId = "";
        do
        {
            Console.Write("\nDigite o ID da tarefa que deseja editar: ");
            todoId = Console.ReadLine();

            try
            {
                var todoExist = await TodoManager.GetTodoExist(client, int.Parse(todoId));

                if (!todoExist)
                    Console.WriteLine("Não existe tarefa com o ID fornecido. Tente novamente!\n");
            }
            catch (Exception)
            {
                Console.WriteLine("Erro ao buscar a tarefa. Tente novamente!\n");
                break;
            }

            Console.Write("\nDigite a nova descrição da tarefa: ");
            var name = Console.ReadLine();

            try
            {
                await TodoManager.UpdateTask(client, int.Parse(todoId), name);
                Console.WriteLine("Tarefa editada com sucesso!");
                break;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Erro ao editar a tarefa. Tente novamente!\n");
            }
        }
        while (true);
    }

    public static async Task ViewAllTodos(HttpClient client)
    {
        try
        {
            var todos = await TodoManager.GetAllTodos(client);
            
            if (todos.Count == 0)
            {
                Console.WriteLine("\n---------");
                Console.WriteLine("Não há tarefas cadastradas no momento.");
                Console.WriteLine("---------\n");
                return;
            }

            Console.WriteLine("\n---------");
            Console.WriteLine("Lista de tarefas:\n");
            todos.ForEach(todo => {
                var status = todo.isComplete ? "Concluída" : "Incompleta";
                Console.WriteLine($"ID: {todo.Id}, Descrição: {todo.Name}, Status: {status}");
            });
            Console.WriteLine("---------\n");
        }
        catch (Exception)
        {
            Console.WriteLine("Erro ao buscar tarefas. Tente novamente!\n");
        }
    }
}