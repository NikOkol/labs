#include <iostream>
#include <cmath>
using namespace std;


void array_input(double** , const int, const int);
int scan_natural_number();
void array_print(double**, const int);
void matrix_difference(double**, double**, const int);

int main()
{
	setlocale(LC_ALL, "Russian");
	cout << "\nВведите размерность квадратных матриц: ";
	const int n = scan_natural_number();

	cout << "\nВведите элементы матрицы A ";
	double** a_array = new double* [n];
	for (int i = 0; i < n; i++)
	{
		a_array[i] = new double[n];
	}
	array_input(a_array, n, 1);
	cout << "\n A = ";
	array_print(a_array, n);

	cout << "\nВведите элементы матрицы B ";
	double** b_array = new double* [n];
	for (int i = 0; i < n; i++)
	{
		b_array[i] = new double[n];
	}
	array_input(b_array, n, 2);
	cout << "\n B = ";
	array_print(b_array, n);

	matrix_difference(a_array, b_array, n);

	for (int i = 0; i < n; i++)
	{
		delete []a_array[i];
	}
	delete []a_array;

	for (int i = 0; i < n; i++)
	{
		delete[]b_array[i];
	}
	delete[]b_array;
}

void array_input(double** arr,const int n, const int arr_number)
{
	if (arr_number == 1)
	{
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				cout << "\na[" << i+1 << "][" << j+1 <<"] = ";
				cin >> arr[i][j];
			}
		}
	}
	else 
	{
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				cout << "\nb[" << i + 1 << "][" << j + 1 << "] = ";
				cin >> arr[i][j];
			}
		}
	}
}

void array_print(double** arr, const int n)
{

	for (int i = 0; i < n; i++)
	{
		cout << "( ";
		for (int j = 0; j < n; j++)
		{
			cout << arr[i][j] << " ";
		}
		cout << ")\n     ";
	}
}


void matrix_difference(double** arr_a, double** arr_b, const int n)
{
	cout << "\nВыберите уменьшаемую матрицу: 1 - A ; 2 - B : ";
	int choice = 0;
	while (choice != 1 && choice != 2)
	{
		cin >> choice;
		if (choice != 1 && choice != 2)
		{
			cout << "\nВведено неверное значение. Повторите попытку: ";
		}
	}
	if (choice == 1)
	{
		cout << "\nВыбрана уменьшаемая матрица A. B - вычитаемая.\nC = A - B\n C = ";
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				arr_a[i][j] -= arr_b[i][j];
			}
		}
		array_print(arr_a, n);

	}
	else
	{
		cout << "\nВыбрана уменьшаемая матрица B. A - вычитаемая.\nC = B - A\n C = ";
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				arr_b[i][j] -= arr_a[i][j];
			}
		}
		array_print(arr_b, n);
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