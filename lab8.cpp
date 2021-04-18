
#include <iostream>
#include <cmath>
using namespace std;

double scan_double();

class complex_number {
public:
    // z = x + yi , Re(z) = x, Im(z) = y
    double real_part;
    double imaginary_part;
    double sin_real_part;
    double sin_imaginary_part;


    void print_complex_number(double x, double y, int type)
    {
        switch (type)
        {
        case 1:
            cout << "\nz";
            break;
        case 2:
            cout << "\nsin(z)";
            break;
        }
        if (y > 0 && x != 0) {
            cout << " = " << x << " + " << y << "i";
        }
        if (y == 0) {
            cout << " = " << x;
        }
        if (x == 0 && y != 0) {
            cout << " = " << y << "i";
        }
        if (y < 0 && x != 0) {
            cout << " = " << x << " - " << abs(y) << "i";
        }
    }

    void get_complex_number()
    {
        cout << "\nВведите действительную часть x: ";
        real_part = scan_double();
        cout << "\nВведите мнимую часть y: ";
        imaginary_part = scan_double();
    }

    void sin_calculating()
    {
        // sin(z) = sin(x)ch(y) + i*cos(x)sh(y)
        sin_real_part = sin(real_part) * cosh(imaginary_part);
        sin_imaginary_part = cos(real_part) * sinh(imaginary_part);
        print_complex_number(sin_real_part, sin_imaginary_part, 2);
    }
};



int main()
{
    setlocale(LC_ALL, "Russian");
    complex_number obj;
    obj.get_complex_number();
    obj.print_complex_number(obj.real_part, obj.imaginary_part, 1);
    obj.sin_calculating();
    return 0;
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