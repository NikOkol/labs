using System.Net.Sockets;
namespace ClientForm
{
    public partial class Form1 : Form
    {

        string host = "127.0.0.1";
        int port = 8888;
        TcpClient client = new TcpClient();
        StreamReader? Reader = null;
        StreamWriter? Writer = null;
        string username = "";
        string[] recipient_key = { "", "" };
        public Form1()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                client.Connect(host, port);
                Reader = new StreamReader(client.GetStream());
                Writer = new StreamWriter(client.GetStream());
                if (Writer is null || Reader is null) return;
                ReceiveMessageAsync(Reader);
                Print("Введите имя пользователя");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }

        }


        private void Print(string msg)
        {
            listBox1.Items.Add(msg);
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;
            textBox1.Text = "";
            if (message == null || message == "") return;
            if (message[0] == '/')
            {
                Print("Символ '/' в начале сообщения запрещён!");
                return;
            }
            if(username == "")
            {
                await Writer.WriteLineAsync(message);
                await Writer.FlushAsync();
                return;
            }
            (recipient_key[0], recipient_key[1]) = ("", "");
            await Writer.WriteLineAsync("/getkey");
            await Writer.FlushAsync();
            await Task.Delay(1);
            if (recipient_key[0] != "" && recipient_key[1] != "" )
            {
                Print($"-> {comboBox1.SelectedItem.ToString()}: {message}");
                message = RSA.Encrypt(message, int.Parse(recipient_key[0]), int.Parse(recipient_key[1]));
                await Writer.WriteLineAsync(message);
                await Writer.FlushAsync();
                
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
                    if (message[0] == '/')
                    {
                        (int command, string param) = CommandSelect(message);
                        switch (command)
                        {
                            case 1: // Добавить в список подключившегося пользователя
                                comboBox1.Items.Add(param);
                                break;

                            case 2: // Удалить из списка пользователей отключившегося пользователя
                                comboBox1.Items.Remove(param);
                                break;
                            case 3: // Загрузить список пользователей
                                string[] users = param.Split(' ');
                                foreach (string user in users)
                                {
                                    comboBox1.Items.Add(user);
                                }
                                break;
                            
                            case 4: // Установить имя пользователя
                                username = param;
                                label6.Text += username;
                                break;
                            case 5: // Получить открытый ключ другого пользователя
                                recipient_key = param.Split(' ');
                                break;
                            case 6: // Расшифровка сообщения
                                Print(param);
                                break;
                            case 0:
                                break;
                        }
                    }
                    else
                    {
                        Print(message);
                    }
                        
                }
                catch
                {
                    break;
                }
            }
        }


        
        async private void button2_Click(object sender, EventArgs e) // генерация ключа
        {
            int p = RSA.RandPrime();
            int q = RSA.RandPrime();
            while(p == q)
            {
                q = RSA.RandPrime();
            }
            int n = p * q;
            int E_func = (p - 1)*(q - 1);
            int public_e = RSA.RandE(p, q);
            int d = RSA.invmod(public_e, E_func);
            textBox2.Text = n.ToString();
            textBox3.Text = public_e.ToString();
            textBox4.Text = d.ToString();
            string openKey = $"/setkey {textBox2.Text} {textBox3.Text}"; // установить ключ
            await Writer.WriteLineAsync(openKey);
            await Writer.FlushAsync();
        }
        
        private (int, string) CommandSelect(string message)
        {
            message = message.Trim(new char[] { '/', ' ' });
            string[] args = message.Split(' ');
            switch (args[0])
            {
                case "adduser":
                    return (1, args[1]);

                case "removeuser":
                    return (2, args[1]);

                case "userlist":
                    if (args.Length == 1) return (0, "");
                    string users = "";
                    for(int i = 1; i < args.Length; i++)
                    {
                        users += args[i] + " ";
                    }
                    return (3, users.Trim());

                case "loggedon":
                    return (4, args[1]);

                case "setkey":
                    return (5, $"{args[1]} {args[2]}");

                case "decrypt":
                    string code = "";
                    for(int i = 2; i < args.Length; i++)
                    {
                        code += args[i] + " ";
                    }
                    string msg = RSA.Decrypt(code.Trim(), int.Parse(textBox2.Text), int.Parse(textBox4.Text));
                    msg = args[1] + ": " + msg;
                    return (6, msg);

                default:
                    return (0, "");
            }

        }

        async private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;
            string selectedUser = comboBox1.SelectedItem.ToString();
            await Writer.WriteLineAsync("/set " + selectedUser);
            await Writer.FlushAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            host = textBox5.Text;
            try
            {
                client.Connect(host, port);
                Reader = new StreamReader(client.GetStream());
                Writer = new StreamWriter(client.GetStream());
                if (Writer is null || Reader is null) return;
                ReceiveMessageAsync(Reader);
                Print("Введите имя пользователя");
            }
            catch (Exception ex)
            {
                Print(ex.Message);
            }
        }
    }
}