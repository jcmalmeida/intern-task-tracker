using System;
using System.Net.Http;
using System.Threading.Tasks;
using TodoApiCLI;

var baseUrl = "http://todoapi:5258";

using var client = new HttpClient { BaseAddress = new Uri(baseUrl) };

Console.WriteLine("\n\nBem-vindo ao gerenciador de tarefas!");
var os = CliManager.DetectOS();
Console.WriteLine($"Seu sistema operacional é: {os}");

var userInput = "";
do {
    Console.WriteLine("\nOpções disponíveis:\n");
    Console.WriteLine("1. Adicionar tarefa");
    Console.WriteLine("2. Remover tarefa");
    Console.WriteLine("3. Marcar tarefa como completada");
    Console.WriteLine("4. Editar uma tarefa");
    Console.WriteLine("5. Ver todas as tarefas");
    Console.WriteLine("6. Encerrar aplicação");
    Console.Write("\nDigite a opção desejada: ");

    userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":
            await CliManager.AddTodo(client);
            break;
        case "2":
            await CliManager.DeleteTodo(client);
            break;
        case "3":
            await CliManager.MarkTodoAsCompleted(client);
            break;
        case "4":
            await CliManager.UpdateTodo(client);
            break;
        case "5":
            await CliManager.ViewAllTodos(client);
            break;
        case "6":
            break;
        default:
            Console.WriteLine("Opção inválida, tente novamente!\n");
            break;
    }
}
while (userInput != "6");

Console.WriteLine("\nObrigado por usar este programa!\n\n");
