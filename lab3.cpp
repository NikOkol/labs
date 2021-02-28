// | Окольников Н. | Лабораторная 3 | Вариант 10 |
#include <iostream>
#include <cmath>
using namespace std;

double scan_double();
int scan_natural_number();
int scan_integer();


int main()
{
	setlocale(LC_ALL, "Russian");
	double number_array[20];
	double chance_array[20];
	cout << "\nВведите порядок k: ";
	int k = scan_integer();
	cout << "\nВведите количество случайных значений(не более 20): ";
	int n = scan_natural_number();
	double max_chance = 1;
	for (int i = 1 ; i <= n; i++) // Заполнение массива
	{
		cout << "\nВведите значение E" << i << ": ";
		number_array[i] = scan_double(); 
		cout << "\nВведите вероятность p" << i << ": ";
		if (i != n) // Если элемент не последний
		{
			chance_array[i] = scan_double();
			while (chance_array[i] > max_chance)
			{
				cout << "\nОшибка: максимальная вероятность превышена! (осталось " << max_chance << ") \nПовторите ввод: ";
				chance_array[i] = scan_double();
			}
			max_chance -= chance_array[i];
			if (max_chance == 0)
			{
				cout << "\nМаксимальная вероятность исчерпана. Остальным значениям присвоить вероятность невозможно.";
				n = i;
				break;
			}
			else {}
		}
		else // Если элемент последний и вероятность не исчерпана
		{
			cout << "Последнему элементу автоматически присваивается оставшаяся вероятность: " << max_chance;
			chance_array[i] = max_chance;
			max_chance = 0;
		}
	}
	
	double sum_central_moment = 0;
	double sum_expectation = 0; 

	for (int i = 1; i <= n; i++) // Математическое ожидание
	{
		sum_expectation = number_array[i] * chance_array[i];
	}

	for (int i = 1; i <= n; i++) // Центральный момент порядка k
	{
		sum_central_moment = pow((number_array[i] - sum_expectation), k) * chance_array[i];
	}
	cout << "\nПорядок момента = " << sum_central_moment;
	return 0;
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
		{
			return input_data;
		}
	}
}

int scan_natural_number() // Получить натурально число
{
	while (true)
	{
		int input_data;
		cin >> input_data;

		if (cin.fail() || input_data <= 0)  // Проверка на натуральное число
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

int scan_integer() // Получить целое число
{
	while (true)
	{
		int input_data;
		cin >> input_data;

		if (cin.fail())  // Проверка на целое число
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
