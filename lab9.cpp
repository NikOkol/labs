#include <iostream>
#include <string>
using namespace std;

int scan_natural();

struct long_cout
{
    string main_menu = "Выберите действие:\n1 - Вывести краткий список\n2 - Вывести подробный список\n3 - Добавить объекты\n4 - Показать информацию об отдельном объекте\n5 - Редактировать объекты\n6 - Завершить программу\nВаш выбор: ";
    string edit_menu = " Выберите поле для редактирования: \n 1 - Название продукта  2 - Производитель  3 - Цена\n 4 - Название магазина  5 - Адрес магазина  6 - Вермя работы магазина\n 7 - Показать текущую информацию  8 - Главное меню  9 - Выбрать другой объект\nВаш выбор: ";
}long_string;

class shop
{
public:
    string shop_name;
    string address;
    string working_hours = "Не определено";
    shop* next_shop = nullptr;
    shop()
    {
        cout << "\nВведите название магазина: ";
        getline(cin, shop_name);
        cout << "Введите адрес магазина: ";
        getline(cin, address);
    }
    
};

class product
{
public:
    int str_number;
    string product_name;
    string manufacturer = "Не определено";
    string price = "Не определено";
    shop* place_of_sale = nullptr;
    product* next_prod = nullptr;
    void show_prod_info()
    {
        cout << "\n" << str_number << ") Название: " << product_name << "\n   Производитель: " << manufacturer << "\n   Цена: " << price << "\n   Где найти: '" << place_of_sale->shop_name << "' , "<< place_of_sale->address << "\n   Когда: " << place_of_sale->working_hours << endl << endl;
    }
    product(int number)
    {
        str_number = number;
        cout << "\nВведите название продукта: ";
        getline(cin, product_name);
        place_of_sale = new shop;
    }

};


void show_long_list(int, product*);
int create_object(product*, int, product**);
void show_short_list(int, product*);
void command_list(product*);
void show_one_object(int, product*);
void object_editing(int, product*);
void clear_memory(int, product*);



int main()
{
    setlocale(LC_ALL, "Russian");
    product* first;
    first = new product(1);
    command_list(first);
    cout << "\nЗавершение работы\n";
    return 0;
}

int create_object(product* last, int n, product** ptr_last) // Добавление новых объектов
{
    cout << "\nСколько объектов добавить? ";
    int k;
    k = scan_natural();
    product* current, * next = nullptr;
    current = last;
    cin.ignore(32767, '\n');
    for (int i = 1; i <= k; i++)
    {
        n++;
        next = new product(n);
        current->next_prod = next;
        current->place_of_sale->next_shop = next->place_of_sale;
        current = next;
    }
    *ptr_last = next;
    return n;
}

void command_list(product * first) // Выбор действия
{
    product* last;
    last = first;
    int choice, n = 1;
    cout << long_string.main_menu;
    choice = scan_natural();
    bool going = true;
    while (going == true)
    {
        switch (choice)
        {
        case 1: // Краткий список
            show_short_list(n, first);
            cout << long_string.main_menu;
            choice = scan_natural();
            break;
        case 2: // Подробный список
            show_long_list(n, first);
            cout << long_string.main_menu;
            choice = scan_natural();
            break;
        case 3: // Добавить объект
            n = create_object(last, n, &last);
            cout << long_string.main_menu;
            choice = scan_natural();
            break;
        case 4: // Информация о конкретном объекте
            show_one_object(n, first);
            cout << long_string.main_menu;
            choice = scan_natural();
            break;
        case 5: // Редактирование объектов
            object_editing(n, first);
            cout << long_string.main_menu;
            choice = scan_natural();
            break;
        case 6: // Завершить программу
            going = false;
            clear_memory(n, first);
            break;
        default:
            cout << "\nВведено неверное значение. Повторите попытку: ";
            choice = scan_natural();
            break;
        }
    }
}

void show_short_list(int n, product* first) // Краткий список
{
    product* current;
    current = first;
    for (int i = 1; i <= n; i++)
    {
        for (int j = 0; j < 51; j++)
        {
            cout << "-";
        }
        cout << "\n| " << current->str_number << ") " << current->product_name;
        for (unsigned int j = 0; j < (20 - (current->product_name.length())); j++)
        {
            cout << " ";
        }
        cout << "| " << current->place_of_sale->shop_name;
        for (unsigned int j = 0; j < (22 - (current->place_of_sale->shop_name.length())); j++)
        {
            cout << " ";
        }
        cout << "|\n";
        for (unsigned int j = 0; j < 51; j++)
        {
            cout << "-";
        }
        cout << endl;
        current = current->next_prod;
    }
}

void show_long_list(int n, product* first) // Подробный список
{
    product* current;
    current = first;
    for (int i = 1; i <= n; i++)
    {
        for (int j = 0; j < 90; j++)
        {
            cout << "-";
        }
        cout << "\n| " << current->str_number << ") Название продукта: " << current->product_name;
        for (unsigned int j = 0; j < (20 - (current->product_name.length())); j++)
        {
            cout << " ";
        }
        cout << "|  Название магазина: " << current->place_of_sale->shop_name;
        for (unsigned int j = 0; j < (19 - (current->place_of_sale->shop_name.length())); j++)
        {
            cout << " ";
        }
        cout << "|\n";
        for (int j = 0; j < 90; j++)
        {
            cout << "-";
        }
        cout << "\n| " <<  "   Производитель: " << current->manufacturer;
        for (unsigned int j = 0; j < (24 - (current->manufacturer.length())); j++)
        {
            cout << " ";
        }
        cout << "|  Адрес магазина: " << current->place_of_sale->address;
        for (unsigned int j = 0; j < (22 - (current->place_of_sale->address.length())); j++)
        {
            cout << " ";
        }
        cout << "|\n";
        for (int j = 0; j < 90; j++)
        {
            cout << "-";
        }
        cout << "\n| " << "   Цена: " << current->price;
        for (unsigned int j = 0; j < (33 - (current->price.length())); j++)
        {
            cout << " ";
        }
        cout << "|  Время работы: " << current->place_of_sale->working_hours;
        for (unsigned int j = 0; j < (24 - (current->place_of_sale->working_hours.length())); j++)
        {
            cout << " ";
        }
        cout << "|\n";
        for (int j = 0; j < 90; j++)
        {
            cout << "-";
        }
        cout << endl;
        current = current->next_prod;
    }
}

void show_one_object(int n, product* first) // Информация об одном объекте
{
    cout << "Выберите номер объекта: ";
    int k = scan_natural();
    while (k > n)
    {
        cout << "\nОбъекта с таким номером не существует. Повторите попытку: ";
        k = scan_natural();
    }
    product* current = first;
    while (current->str_number != k)
    {
        current = current->next_prod;
    }
    current->show_prod_info();
}

void object_editing(int n, product* first) // Редактирование объектов
{
    cout << "Выберите номер объекта: ";
    int k = scan_natural();
    while (k > n)
    {
        cout << "\nОбъекта с таким номером не существует. Повторите попытку: ";
        k = scan_natural();
    }
    product* current = first;
    while (current->str_number != k)
    {
        current = current->next_prod;
    }
    cout << long_string.edit_menu;
    bool going = true;
    int choice = scan_natural();

    while (going == true)
    {
        switch (choice)
        {
        case 1: // Название продукта
            cin.ignore(32767, '\n');
            getline(cin, current->product_name);
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 2: // Производитель
            cin.ignore(32767, '\n');
            getline(cin, current->manufacturer);
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 3: // Цена
            cin.ignore(32767, '\n');
            getline(cin, current->price);
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 4: // Название магазина
            cin.ignore(32767, '\n');
            getline(cin, current->place_of_sale->shop_name);
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 5: // Адрес магазина
            cin.ignore(32767, '\n');
            getline(cin, current->place_of_sale->address);
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 6: // Часы работы магазина
            cin.ignore(32767, '\n');
            getline(cin, current->place_of_sale->working_hours);
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 7: // Показать информацию о текущем объекте
            current->show_prod_info();
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        case 8: // В главное меню
            going = false;
            break;
        case 9: // Сменить редактируемый объект
           cout << "Выберите номер объекта: ";
            k = scan_natural();
            while (k > n)
            {
                cout << "\nОбъекта с таким номером не существует. Повторите попытку: ";
                k = scan_natural();
            }
            current = first;
            while (current->str_number != k)
            {
                current = current->next_prod;
            }
            cout << long_string.edit_menu;
            choice = scan_natural();
            break;
        default:
            cout << "\nВведено неверное значение. Повторите попытку: ";
            choice = scan_natural();
            break;
        }
    }
}

void clear_memory(int n, product* first) // Очистка памяти
{
    product* current, * next = nullptr;
    current = first;
    for (int i = 1; i <= n; i++)
    {
        if (i != n)
        {
            next = current->next_prod;
        }
        delete current->place_of_sale;
        delete current;
        current = next;
    }
}

int scan_natural() // Получить натуральное число
{
    while (true)
    {
        int input_data;
        cin >> input_data;
        if (cin.fail() || input_data < 1) 
        {
            cout << "\nВведено неверное значение. Повторите попытку: ";
            cin.clear();
            cin.ignore(32767, '\n');
        }
        else
        {
            return input_data;
        }
    }
}
