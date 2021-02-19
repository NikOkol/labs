// | Окольников Н. | Лабораторная 1 | Вариант 10 |
#include <iostream>
using namespace std;

double acceleration(double h);
double scan_double();

int main()
{
	setlocale(LC_ALL, "Russian");
	cout << "Введите высоту над поверхностью земли (в метрах): ";
	double height = scan_double();
	double g = acceleration(height);
	cout << "\n”Ускорение свободного падения равно " << g << " м/с^2\n";
}

double acceleration(double h) // Расчёт ускорения свободного падения
{
	double grav_constant = 6.672 * pow (10, -11);
	double earth_weight = 5.96 * pow(10, 24);
	double earth_radius = 6.37 * pow(10, 6);
	double g = grav_constant * earth_weight / pow((earth_radius + h), 2);
	return(g);
}

double scan_double() // Получить тип данных dobule
{
	while (true)
	{
		double input_data;
		cin >> input_data;

		if (cin.fail())  // Проверка на double
		{
			cout << "\nВведено неверное значение. Повторите попытку: ";
			cin.clear(); 
			cin.ignore(32767, '\n'); 
		}
		else 
			return input_data;
	}
}
