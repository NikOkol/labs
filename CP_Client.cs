using System.Net.Sockets;

string host = "127.0.0.1";
int port = 8888;
using TcpClient client = new TcpClient();
Console.Write("Enter your username: ");
string? userName = Console.ReadLine();
StreamReader? Reader = null;
StreamWriter? Writer = null;

try
{
    client.Connect(host, port);
    Reader = new StreamReader(client.GetStream());
    Writer = new StreamWriter(client.GetStream());
    if (Writer is null || Reader is null) return;
    Task.Run(() => ReceiveMessageAsync(Reader));
    await SendMessageAsync(Writer);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Writer?.Close();
Reader?.Close();

async Task SendMessageAsync(StreamWriter writer)
{
    await writer.WriteLineAsync(userName);
    await writer.FlushAsync();

    while (true)
    {
        string? message = Console.ReadLine();
        await writer.WriteLineAsync(message);
        await writer.FlushAsync();
    }
}
async Task ReceiveMessageAsync(StreamReader reader)
{
    while (true)
    {
        try
        {
            string? message = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(message)) continue;
            Print(message);
        }
        catch
        {
            break;
        }
    }
}
void Print(string message)
{
    if (OperatingSystem.IsWindows())
    {
        var position = Console.GetCursorPosition();
        int left = position.Left;
        int top = position.Top;
        Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
        Console.SetCursorPosition(0, top);
        Console.WriteLine(message);
        Console.SetCursorPosition(left, top + 1);
    }
    else Console.WriteLine(message);
}