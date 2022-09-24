#include <iostream>

using namespace std;

int main()
{
    struct A
    {
        int key;
        string firstname;
        string secondname;
        int birthyear;
        A* next;
    };


    A* first, * current, * last;
    first = new A;
    current = first;
    last = nullptr;

    first->key = 1;
    first->firstname = "Ivan";
    first->secondname = "Bobrov";
    first->birthyear = 1999;


    int choice = 0;
    int n = 1;

    while (choice != 3)
    {
        cout << "----------- Menu -----------" << endl;
        cout << "1 - Add line" << endl;
        cout << "2 - Edit line" << endl;
        cout << "3 - Exit" << endl;
        cout << "4 - Show list" << endl;

        cin >> choice;

        switch (choice)
        {
        case 1:
            last = new A;
            current = first;
            for (int i = 1; i < n; i++)
            {
                current = current->next;
            }
            current->next = last;
            current = last;
            cout << "Enter the key" << endl;
            cin >> current->key;
            cout << "Enter the first name" << endl;
            cin >> current->firstname;
            cout << "Enter the second name" << endl;
            cin >> current->secondname;
            cout << "Enter the year of birth" << endl;
            cin >> current->birthyear;
            n++;

            break;

        case 2:
            cout << "Enter number of line" << endl;
            int line_number;
            cin >> line_number;
            current = first;

            for (int i = 1; i < line_number; i++)
            {
                current = current->next;
            }

            cout << "Enter the key" << endl;
            cin >> current->key;
            cout << "Enter the first name" << endl;
            cin >> current->firstname;
            cout << "Enter the second name" << endl;
            cin >> current->secondname;
            cout << "Enter the year of birth" << endl;
            cin >> current->birthyear;

            break;

        case 4:
            current = first;
            for (int i = 1; i <= n; i++)
            {
                cout << current->key << " | " << current->firstname << " | " << current->secondname << " | " << current->birthyear << endl;
                if (i != n)
                {
                    current = current->next;
                }
            }
            break;

        default:
            break;

            
        }
    }
}
