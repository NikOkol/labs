using System.Net;
using System.Net.Sockets;

ServerObject server = new ServerObject();
await server.ListenAsync(); 

class ServerObject
{
    TcpListener tcpListener = new TcpListener(IPAddress.Any, 8888); 
    List<ClientObject> clients = new List<ClientObject>();
    protected internal void RemoveConnection(string id)
    {
       
        ClientObject? client = clients.FirstOrDefault(c => c.Id == id);
        SendGlobalMessageAsync($"/removeuser {client.userName}", id);
        if (client != null) clients.Remove(client);
        client?.Close();
    }
   
    protected internal async Task ListenAsync()
    {
        try
        {
            tcpListener.Start();
            Console.WriteLine("Server started");

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                ClientObject clientObject = new ClientObject(tcpClient, this);
                clients.Add(clientObject);
                Task.Run(clientObject.ProcessAsync);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Disconnect();
        }
    }

   
    protected internal async Task SendMessageAsync(string message, string id)
    {
        foreach (var client in clients)
        {
            if (client.Id == id) 
            {
                await client.Writer.WriteLineAsync(message);
                await client.Writer.FlushAsync();
            }
        }
    }

    protected internal async Task SendGlobalMessageAsync(string message, string id)
    {
        foreach (var client in clients)
        {
            if (client.Id != id && client.userName != "")
            {
                await client.Writer.WriteLineAsync(message);
                await client.Writer.FlushAsync();
            }
        }
    }

    protected internal List<ClientObject> GetClientList()
    {
        return clients;
    }

    protected internal void Disconnect()
    {
        foreach (var client in clients)
        {
            client.Close(); 
        }
        tcpListener.Stop(); 
    }
}
class ClientObject
{
    protected internal string Id { get; } = Guid.NewGuid().ToString();
    protected internal string userName { get; set; } 
    protected internal StreamWriter Writer { get; }
    protected internal StreamReader Reader { get; }
    protected internal string[] openKey { get; set; }

    TcpClient client;
    ServerObject server; 

    public ClientObject(TcpClient tcpClient, ServerObject serverObject)
    {
        client = tcpClient;
        server = serverObject;
        var stream = client.GetStream();
        Reader = new StreamReader(stream);
        Writer = new StreamWriter(stream);
        userName = "";
        openKey = new string[] { "", "" };
    }

    public async Task ProcessAsync()
    {
        try
        {
            List<ClientObject> clientList = server.GetClientList();
            while (true)
            {
                
                string? tryName = await Reader.ReadLineAsync();
                tryName = tryName.Trim();
                clientList = server.GetClientList();
                if (tryName == null || tryName == "") continue;
                if (tryName.Length > 15)
                {
                    await Writer.WriteLineAsync("Имя слишком длинное. Попробуйте снова");
                    await Writer.FlushAsync();
                    continue;
                }
                if (tryName.Contains(' '))
                {
                    await Writer.WriteLineAsync("Имя не должно содержать пробелов");
                    await Writer.FlushAsync();
                    continue;
                }
                bool IsExist = false;
                foreach (var client in clientList)
                {
                    if (tryName == client.userName)
                    {
                        await Writer.WriteLineAsync("Имя уже занято. Попробуйте снова");
                        await Writer.FlushAsync();
                        IsExist = true;
                    }
                }
                if (IsExist) continue;
                userName = tryName;
                await Writer.WriteLineAsync($"Добро пожаловать, {userName}!");
                await Writer.FlushAsync();
                break;
            }
            string? message = $"{userName} joined the server";
            Console.WriteLine(message);
            message = "/loggedon " + userName;
            await Writer.WriteLineAsync(message);
            await Writer.FlushAsync();
            await server.SendGlobalMessageAsync($"/adduser {userName}", Id);
            ClientObject messageTarget = null;
            message = "/userlist";
            foreach(var client in clientList)
            {
                if (client.userName == userName) continue;
                message += " " + client.userName;
            }
            await Writer.WriteLineAsync(message);
            await Writer.FlushAsync();

            while (true)
            {
                try
                {
                    message = await Reader.ReadLineAsync();
                    clientList = server.GetClientList();
                    if (message == null) continue;
                    if (message[0] == '/')
                    {
                        (int command, string param) = CommandSelect(message);
                        switch (command)
                        {
                            case 1:
                                foreach (var client in clientList)
                                {
                                    if (client.userName == param)
                                    {
                                        messageTarget = client;
                                        break;
                                    }
                                }
                                break;
                            case 2:
                                if (messageTarget == null)
                                {
                                    await Writer.WriteLineAsync("Получатель не выбран");
                                    await Writer.FlushAsync();
                                }
                                else
                                {
                                    if (messageTarget.openKey[0] == "")
                                    {
                                        await Writer.WriteLineAsync("Получатель не установил ключ");
                                        await Writer.FlushAsync();
                                        await messageTarget.Writer.WriteLineAsync("Вам пытались отправить сообщение, но ключ не установлен");
                                        await messageTarget.Writer.FlushAsync();
                                    }
                                    else
                                    {
                                        await Writer.WriteLineAsync($"/setkey {messageTarget.openKey[0]} {messageTarget.openKey[1]}");
                                        await Writer.FlushAsync();
                                    }
                                }
                                break;
                            case 3:
                                string[] args = param.Split(' ');
                                openKey = args;
                                break;

                            case 0:
                                await Writer.WriteLineAsync(param);
                                await Writer.FlushAsync();
                                break;

                        }
                    }
                    else
                    {
                        await server.SendMessageAsync($"/decrypt {userName} {message}", messageTarget.Id);
                    }
                    message = $"{userName}: {message}";
                    Console.WriteLine(message);
                }
                catch
                {
                    message = $"{userName} left server";
                    Console.WriteLine(message);
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            server.RemoveConnection(Id);
        }
    }


    private (int, string) CommandSelect(string message)
    {
        message = message.Trim(new char[] { '/', ' ' });
        string[] args = message.Split(' ');
        switch(args[0])
        {
            case "set":
                return (1, args[1]);
            case "getkey":
                return (2, "");
            case "setkey":
                return (3, $"{args[1]} {args[2]}");


            default: 
                return (0, "Command doesn't exist");
        }

    }
    protected internal void Close()
    {
        Writer.Close();
        Reader.Close();
        client.Close();
    }
}