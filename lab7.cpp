#include <iostream>
#include <cmath>
using namespace std;

double scan_double();

class Calculating {
public:
    double a;
    double b;
    double c;
    double D;
    double x1, x2;
    void discriminant()
    {
        D =  pow(b, 2) - 4 * a * c;
    }
    void root_small() 
    {
        if (b > 0) 
        {
            x1 = (b * (-1) - sqrt(D)) / (a * 2);
        }
        else 
        {
            x2 = (b * (-1) - sqrt(D)) / (a * 2);
        }
        
    }
    void root_big()
    {
        if (b > 0) 
        {
            x2 = (b * (-1) + sqrt(D)) / (a * 2);
        }
        else 
        {
            x1 = (b * (-1) - sqrt(D)) / (a * 2);
        }
        
    }

};




int main()
{
    setlocale(LC_ALL, "Russian");
    Calculating Obj;
    cout << "\nУравнение: ax^2 + bx + c = 0\nВведите коэффициенты\n";
    do {
        cout << "a = ";
        Obj.a = scan_double();
        cout << "b = ";
        Obj.b = scan_double();
        cout << "c = ";
        Obj.c = scan_double();
        if (Obj.a == 0 && Obj.b == 0 && Obj.c == 0) 
        {
            cout << "\nВсе коэффициенты равны нулю! Повторите ввод.\n";
        }
        if (Obj.a == 0 && Obj.b == 0)
        {
            cout << "\nКоэффициенты при x равны нулю! Повторите ввод.\n";
        }
    } while (Obj.a == 0 && Obj.b == 0);
    Obj.discriminant();
    if (Obj.D < 0)
    {
        cout << "\nКорней нет! ";
        return 0;
    }
    else
    {
        if (Obj.a == 0 && Obj.c == 0)
        {
            cout << "\nx - любое";
            return 0;
        }
        Obj.root_small();
        Obj.root_big();
        cout << "\n x1 = " << Obj.x1 << "  x2 = " << Obj.x2;
    }
}

double scan_double()
{
    while (true)
    {
        double input_data;
        cin >> input_data;

        if (cin.fail())  
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