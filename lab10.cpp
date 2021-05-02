
#include <iostream>
#include <cmath>
using namespace std;

int scan_natural();
double scan_double();

class Operator_overload
{
public:
    int n;
    double *a;
    double sum = 0;
    double x;
    bool is_x_defined;
    double derivative;
    void arr_allocate()
    {
        cout << "\nВведите n: ";
        n = scan_natural();
        a = new double[n+1];
        bool going = true;
        while (going == true)
        {
            cout << "\nВыберите способ ввода элементов а: 1 - Вручную  2 - Случайно" << endl;
            int choice = scan_natural();
            switch (choice)
            {
            case 1:
                for (int i = 0; i <= n; i++)
                {
                    cout << "\na[" << i << "] = ";
                    a[i] = scan_double();
                }
                going = false;
                break;
            case 2:
                for (int i = 0; i <= n; i++)
                {
                    a[i] = (double)(-100 + rand() % 200);
                }
                going = false;
                break;
            default:
                cout << "\nВведено неверное значение. Повторите попытку: ";
                break;
            }
        }
    }
    void arr_print()
    {
        for (int i = 0; i <= n; i++)
        {
            cout << "\na[" << i << "] = " << a[i];
        }
    }
    void sum_calculate(double ptr_x)
    {
        for (int i = 0; i <= n; i++)
        {
            sum += a[i] * exp(-i * x);
        }
        cout << "\nQ(x) = " << sum << endl;
        is_x_defined = true;
        x = ptr_x;
    }
    void sum_calculate()
    {
        cout << "\nQ(x) = ";
        for (int i = 0; i <= n; i++)
        {
            cout << a[i] << "*e^(-" << i << "x) ";
            if (i != n)
            {
                cout << "+ ";
            }
            is_x_defined = false;
        }
    }
    Operator_overload operator^ (int order)
    {
        if (is_x_defined == true)
        {
            double* b = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                b[i] = a[i] * (-1) * i;
            }
            for (int j = 1; j <= order; j++)
            {
                derivative = 0;
                for (int i = 0; i <= n; i++)
                {
                    derivative += b[i] * exp(-i*x);
                    b[i] *= -i;
                }
                cout << "\nПроизводная порядка " << j << " = " << derivative;
            }
            delete[] b;
        }
        else
        {
            double* b = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                b[i] = a[i] * (-1) * i;
            }
            for (int i = 1; i <= order; i++)
            {
                cout << "\nПроизводная порядка " << i << " = ";
                for (int j = 0; j <= n; j++)
                {
                    cout << b[j] << "*e^(-" << j << "x) ";
                    if (j != n)
                    {
                        cout << "+ ";
                    }
                    b[j] *= -i;
                }
                
            }
            delete[] b;
        }
        return *this;
    }
    void arr_deallocate()
    {
        delete[] a;
    }
};

int main()
{
    srand(time(0));
    setlocale(LC_ALL, "Russian");
    Operator_overload obj1;
    obj1.arr_allocate();
    obj1.arr_print();
    cout << "\nВведите x (если x не определён, введите любой знак, не являющийся числом): ";
    double x;
    cin >> x;
    if (cin.fail())
    {
        cin.clear();
        cin.ignore(32767, '\n');
        obj1.sum_calculate();
    }
    else
    {
        obj1.sum_calculate(x);
    }
    cout << "\nВведите порядок производной: ";
    int order = scan_natural();
    obj1 ^ order;
    obj1.arr_deallocate();
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

double scan_double() // Получить тип данных dobule
{
    while (true)
    {
        double input_data;
        cin >> input_data;

        if (cin.fail() || input_data < 0)  // Проверка на double и на положительное число
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
