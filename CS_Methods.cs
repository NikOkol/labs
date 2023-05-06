static byte BinaryStringToByte(string str) // Ïðåîáðàçîâàíèå ñòðîêè áèíàðíîãî êîäà â áàéò
{
    byte b = 0;
    for (int i = str.Length <= 7 ? str.Length - 1 : 7; i >= 0; i--)
    {
        if (str[i] == '1')
        {
            b += (byte)Math.Pow(2, str.Length <= 7 ? str.Length - 1 - i : 7 - i);
        }
    }
    return b;
}

static int BinaryStringToInt(string str) // Ïðåîáðàçîâàíèå ñòðîêè áèíàðíîãî êîäà â int
{
    int value = 0;
    for (int i = str.Length - 1; i >= 0; i--)
    {
        if (str[i] == '1')
        {
            b += Math.Pow(2, str.Length - 1 - i);
        }
    }
    return b;
}

static string PathInput() // Ïðîâåðêà íà ñóùåñòâîâàíèå ôàéëà
{
    while (true)
    {
        string input = Console.ReadLine();
        if (File.Exists(input))
        {
            return input;
        }
        else
        {
            Console.WriteLine("File does not exist. Try again");
        }
    }
}

public static int Int_input() // ×òåíèå integer
{
    while (true)
    {
        int digit;
        if (int.TryParse(Console.ReadLine(), out digit))
        {
            return digit;
        }
        else
        {
            Console.WriteLine("Incorrect value");
        }
    }
}

static byte[] Append(byte[] array, byte item) // Ðàñøèðåíèå ìàññèâà áàéòîâ
{
    byte[] result = new byte[array.Length + 1];
    array.CopyTo(result, 0);
    result[array.Length] = item;
    return result;
}

static int GetKeyByValue(char value) // Ïîëó÷èòü èç àëôàâèòà êëþ÷ ïî áóêâå
{
    foreach (var recordOfDictionary in Alphabet)
    {
        if (recordOfDictionary.Key.Equals(value))
            return recordOfDictionary.Value;
    }
    return -1;
}

static char GetValueByKey(int key) // Ïîëó÷èòü èç àëôàâèòà áóêâó ïî êëþ÷ó
{
    foreach (var recordOfDictionary in Alphabet)
    {
        if (recordOfDictionary.Value.Equals(key))
            return recordOfDictionary.Key;
    }
    return '-';
}

static Dictionary<char, int> Alphabet = new Dictionary<char, int>
        {
            {'À', 10},
            {'Á', 11},
            {'Â', 12},
            {'Ã', 13},
            {'Ä', 14},
            {'Å', 15},
            {'Æ', 16},
            {'Ç', 17},
            {'È', 18},
            {'É', 19},
            {'Ê', 20},
            {'Ë', 21},
            {'Ì', 22},
            {'Í', 23},
            {'Î', 24},
            {'Ï', 25},
            {'Ð', 26},
            {'Ñ', 27},
            {'Ò', 28},
            {'Ó', 29},
            {'Ô', 30},
            {'Õ', 31},
            {'Ö', 32},
            {'×', 33},
            {'Ø', 34},
            {'Ù', 35},
            {'Ú', 36},
            {'Û', 37},
            {'Ü', 38},
            {'Ý', 39},
            {'Þ', 40},
            {'ß', 41},
            {' ', 99}
        };

static int ReciprocalNumber(int a, int m) // Ïîèñê îáðàòíîãî ýëåìåíòà ïî ìîäóëþ m
{
    int x = 1;
    while (((a * x) % m) != 1)
    {
        x++;
        if (x > m)
        {
            Console.WriteLine("Error: Reciprosal does not exist");
            return 0;
        }
    }
    return x;
}

static string NotNullStringInput() // Ïðîâåðêà íà íåíóëåâóþ ñòðîêó
{
    string input = Console.ReadLine();
    while (input == "")
    {
        Console.WriteLine("Enter valid value");
        input = Console.ReadLine();
    }
    return input;
}

static char OftenElement(string str) // Ïîèñê ñàìîé ÷àñòîé áóêâû â ñòðîêå
{
    char[] arr = str.ToCharArray();
    int count, count_max = 0;
    char max_el = 'a';
    foreach (char c in arr) // Äëÿ êàæäîãî ñèìâîëà ñòðîêè
    {
        count = 0;
        foreach (Match m in Regex.Matches(str, c.ToString())) // ñ÷èòàåì, ñêîëüêî ðàç îí ïîÿâëÿåòñÿ â ñòðîêå.
        {
            count++;
        }
        if (count >= count_max) // Åñëè ñèìâîë ïîÿâëÿåòñÿ ÷àùå äðóãèõ, 
        {
            count_max = count;
            max_el = c; // òî îí - ñàìûé ÷àñòûé.
        }


    }
    return max_el;
}

static char[] TopOftenElems(string str) // Ñîñòàâëåíèå òîïà ÷àñòûõ ñèìâîëîâ â çàäàííîé ñòðîêå
{
    char[] top = new char[32];
    int i = 0;
    while (str != "")
    {
        top[i] = OftenElement(str);
        str = str.Replace(top[i].ToString(), "");
        i++;
    }
    return top;
}

static int DivByMod(int m, int c, int d) // Äåëåíèå ïî ìîäóëþ
{
    int x = 0;
    if (d < 0)
    {
        d += m;
    }
    while ((c + x * m) % d != 0)
    {
        x++;
        if (x > m)
        {
            Console.WriteLine("Inf cycle!");
            break;
        }
    }
    return ((c + x * m) / d) + m % m;
}

static string[] FindAllWords(string str) // Ñîñòàâëåíèå ìàññèâà ñòðîê èç âñåõ ñëîâ èç òåêñòà
{
    List<string> words = new List<string>();
    string word = "";
    foreach (char letter in str)
    {
        if (letter != ' ' && letter != '.' && letter != ',' && letter != ':')
        {
            word += letter;
        }
        else
        {
            if (word != "")
            {
                words.Add(word);
            }
            word = "";
        }
    }
    return words.ToArray();
}

static void StartApp()
{
    ProcessStartInfo infoStartProcess = new ProcessStartInfo();
    infoStartProcess.WorkingDirectory = "";
    infoStartProcess.FileName = "";
    Process.Start(infoStartProcess);
}
