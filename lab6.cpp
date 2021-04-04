
#include <iostream>
#include <string>
using namespace std;

int scan_natural_number();

struct second_list
{
    int age;
    second_list* next_second;
};

struct first_list
{
    string name;
    first_list* next_first;
    second_list* to_second;
};



void list_output(first_list*, int);

int main()
{
    setlocale(LC_ALL, "Russian");
    int n = 0;
    string string_input;
    first_list* first_f, * current_f, * last_f;
    second_list* first_s, * current_s, * last_s;
    first_f = new first_list;
    first_s = new second_list;
    current_f = first_f;
    current_s = first_s;
    last_s = nullptr;
    last_f = nullptr;
    cout << "\nЗаполните список имён людей и их возраста(в полных годах). Для завершения введите 'end' ";
    do
    {
        cout << "\n " << n + 1 << ")  Имя: ";
        getline(cin, string_input);
        if (string_input == "end")
        {
            break;
        }
        n++;
        current_f->to_second = current_s;
        current_f->name = string_input;
        cout << "  Возраст: ";
        current_s->age = scan_natural_number();
        last_f = new first_list;
        current_f->next_first = last_f;
        last_s = new second_list;
        current_s->next_second = last_s;
        current_f = last_f;
        current_s = last_s;
        cin.ignore(32767, '\n');
    } while (true);

    current_f->next_first = first_f;
    current_s->next_second = first_s;
    list_output(first_f, n);

    for (int i = 0; i <= n; i++)
    {
        current_f = last_f->next_first;
        delete last_f;
        last_f = current_f;
        current_s = last_s->next_second;
        delete last_s;
        last_s = current_s;
    }
}

void list_output(first_list* first_f, int n)
{
    first_list* cur, * next;
    cur = first_f;
    second_list* cur_s;
    for (int i = 0; i < n; i++)
    {
        next = cur->next_first;
        cur_s = cur->to_second;
        for (int j = 0; j < 51; j++)
        {
            cout << "-";
        }
        cout << "\n" << i + 1 << ")   Имя: " << cur->name;
        for (int j = 0; j < (10- (cur->name.length())); j++)
        {
            cout << " ";
        }
        cout << " | Возраст: " << cur_s->age << "\n";
        for (int j = 0; j < 51; j++)
        {
            cout << "-";
        }
        cout << "\n";
        cur = next;
    }
}



int scan_natural_number() 
{
    while (true)
    {
        int input_data;
        cin >> input_data;

        if (cin.fail() || input_data <= 0) 
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
