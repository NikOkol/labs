static byte BinaryStringToByte(string str) // �������������� ������ ��������� ���� � ����
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

static int BinaryStringToInt(string str) // �������������� ������ ��������� ���� � int
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

static string PathInput() // �������� �� ������������� �����
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

public static int Int_input() // ������ integer
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

static byte[] Append(byte[] array, byte item) // ���������� ������� ������
{
    byte[] result = new byte[array.Length + 1];
    array.CopyTo(result, 0);
    result[array.Length] = item;
    return result;
}

static int GetKeyByValue(char value) // �������� �� �������� ���� �� �����
{
    foreach (var recordOfDictionary in Alphabet)
    {
        if (recordOfDictionary.Key.Equals(value))
            return recordOfDictionary.Value;
    }
    return -1;
}

static char GetValueByKey(int key) // �������� �� �������� ����� �� �����
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
            {'�', 10},
            {'�', 11},
            {'�', 12},
            {'�', 13},
            {'�', 14},
            {'�', 15},
            {'�', 16},
            {'�', 17},
            {'�', 18},
            {'�', 19},
            {'�', 20},
            {'�', 21},
            {'�', 22},
            {'�', 23},
            {'�', 24},
            {'�', 25},
            {'�', 26},
            {'�', 27},
            {'�', 28},
            {'�', 29},
            {'�', 30},
            {'�', 31},
            {'�', 32},
            {'�', 33},
            {'�', 34},
            {'�', 35},
            {'�', 36},
            {'�', 37},
            {'�', 38},
            {'�', 39},
            {'�', 40},
            {'�', 41},
            {' ', 99}
        };

static int ReciprocalNumber(int a, int m) // ����� ��������� �������� �� ������ m
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

static string NotNullStringInput() // �������� �� ��������� ������
{
    string input = Console.ReadLine();
    while (input == "")
    {
        Console.WriteLine("Enter valid value");
        input = Console.ReadLine();
    }
    return input;
}

static char OftenElement(string str) // ����� ����� ������ ����� � ������
{
    char[] arr = str.ToCharArray();
    int count, count_max = 0;
    char max_el = 'a';
    foreach (char c in arr) // ��� ������� ������� ������
    {
        count = 0;
        foreach (Match m in Regex.Matches(str, c.ToString())) // �������, ������� ��� �� ���������� � ������.
        {
            count++;
        }
        if (count >= count_max) // ���� ������ ���������� ���� ������, 
        {
            count_max = count;
            max_el = c; // �� �� - ����� ������.
        }


    }
    return max_el;
}

static char[] TopOftenElems(string str) // ����������� ���� ������ �������� � �������� ������
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

static int DivByMod(int m, int c, int d) // ������� �� ������
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

static string[] FindAllWords(string str) // ����������� ������� ����� �� ���� ���� �� ������
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