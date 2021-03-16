#include <iostream>
#include <cmath>
using namespace std;

double scan_double();
void get_coordinates(double**, int);
int scan_natural_number();
double vector_lenght(double*, int);
void angle_calculating(double*, double*, int);

int main()
{
    setlocale(LC_ALL, "Russian");
    cout << "\nВведите размерность пространства: ";
	int dimension = scan_natural_number();
	double* a_vector;
	double* b_vector;
	cout << "\nВведите координаты вектора а: ";
	get_coordinates(&a_vector, dimension);
	cout << "\nВведите координаты вектора b: ";
	get_coordinates(&b_vector, dimension);
	cout << "\nКоординаты вектора а: ";
	for (int i = 1; i <= dimension; i++)
	{
		cout << " " << a_vector[i];
	}
	cout << "\nКоординаты вектора b: ";
	for (int i = 1; i <= dimension; i++)
	{
		cout << " " << b_vector[i];
	}
	angle_calculating(a_vector, b_vector, dimension);
}


void angle_calculating(double *a, double *b, int n) // Вычисление угла
{
	const double PI = 3.141592653589793;
	double scalar = 0;
	for (int i = 1; i <= n; i++)
	{
		scalar += a[i] * b[i];
	}
	double lenght_a = vector_lenght(a, n);
	double lenght_b = vector_lenght(b, n);
	if (lenght_a == 0 || lenght_b == 0)
	{
		cout << "Найден нулевой вектор! Угла нет.";
		return;
	} 
	else {}
	double angle = acos(scalar / (lenght_a * lenght_b)) * 180 / PI;
	if (angle > 90)
	{
		angle = 180 - angle;
	} 
	else {}
	cout << "\nУгол между векторами = " << angle << " градусов";
}


void get_coordinates(double** vector, int n) // Получить координаты вектора
{
	double *vector_ptr = new double[n];
	for (int i = 1; i <= n; i++)
	{
		vector_ptr[i] = scan_double();
	}
	*vector = vector_ptr;
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


double vector_lenght(double *vect, int n)
{
	double sum_of_squares = 0;
	for (int i = 1; i <= n; i++)
	{
		sum_of_squares += pow(vect[i], 2);
	}
	double result = sqrt(sum_of_squares);
	return result;
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