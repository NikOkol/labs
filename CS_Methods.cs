static byte BinaryStringToByte(string str) // Преобразование строки бинарного кода в байт
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

static int BinaryStringToInt(string str) // Преобразование строки бинарного кода в int
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

static string PathInput() // Проверка на существование файла
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

public static int Int_input() // Чтение integer
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

static byte[] Append(byte[] array, byte item) // Расширение массива байтов
{
    byte[] result = new byte[array.Length + 1];
    array.CopyTo(result, 0);
    result[array.Length] = item;
    return result;
}

static int GetKeyByValue(char value) // Получить из алфавита ключ по букве
{
    foreach (var recordOfDictionary in Alphabet)
    {
        if (recordOfDictionary.Key.Equals(value))
            return recordOfDictionary.Value;
    }
    return -1;
}

static char GetValueByKey(int key) // Получить из алфавита букву по ключу
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
            {'А', 10},
            {'Б', 11},
            {'В', 12},
            {'Г', 13},
            {'Д', 14},
            {'Е', 15},
            {'Ж', 16},
            {'З', 17},
            {'И', 18},
            {'Й', 19},
            {'К', 20},
            {'Л', 21},
            {'М', 22},
            {'Н', 23},
            {'О', 24},
            {'П', 25},
            {'Р', 26},
            {'С', 27},
            {'Т', 28},
            {'У', 29},
            {'Ф', 30},
            {'Х', 31},
            {'Ц', 32},
            {'Ч', 33},
            {'Ш', 34},
            {'Щ', 35},
            {'Ъ', 36},
            {'Ы', 37},
            {'Ь', 38},
            {'Э', 39},
            {'Ю', 40},
            {'Я', 41},
            {' ', 99}
        };

static int ReciprocalNumber(int a, int m) // Поиск обратного элемента по модулю m
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

static string NotNullStringInput() // Проверка на ненулевую строку
{
    string input = Console.ReadLine();
    while (input == "")
    {
        Console.WriteLine("Enter valid value");
        input = Console.ReadLine();
    }
    return input;
}

static char OftenElement(string str) // Поиск самой частой буквы в строке
{
    char[] arr = str.ToCharArray();
    int count, count_max = 0;
    char max_el = 'a';
    foreach (char c in arr) // Для каждого символа строки
    {
        count = 0;
        foreach (Match m in Regex.Matches(str, c.ToString())) // считаем, сколько раз он появляется в строке.
        {
            count++;
        }
        if (count >= count_max) // Если символ появляется чаще других, 
        {
            count_max = count;
            max_el = c; // то он - самый частый.
        }


    }
    return max_el;
}

static char[] TopOftenElems(string str) // Составление топа частых символов в заданной строке
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

static int DivByMod(int m, int c, int d) // Деление по модулю
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

static string[] FindAllWords(string str) // Составление массива строк из всех слов из текста
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