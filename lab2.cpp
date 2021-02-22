// | Окольников Н. | Лабораторная 2 | Вариант 10 |
#include <iostream>
#include <cmath> 
using namespace std;
int scan_int();
double composition_function(int);

int main()
{
	setlocale(LC_ALL, "Russian");
	cout << "Введите верхнюю границу(целое число): ";
	int n = scan_int();
	double result = composition_function(n);
	cout << "\nПроизведение равно " << result;
}

int scan_int() // Получить тип данных integer
{
	while (true)
	{
		int input_data;
		cin >> input_data;
		if (cin.fail()) // Проверка на integer
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

double composition_function(int n)
{
	const double PI = 3.141592653589793;
	double composition = 1;
	for (int i = 1; i <= n; i++)
	{
		composition = composition * cos(PI / (pow(2, i + 1)));
	}
	return composition;
}
