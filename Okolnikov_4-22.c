#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#define MAX_SIZE 8

int init_choice();
int init_random_column_size();
int init_random_line_size();
int init_manual_column_size();
int init_manual_line_size();
void init_manual_array(int, int, int[MAX_SIZE][MAX_SIZE]);
void init_random_array(int, int, int[MAX_SIZE][MAX_SIZE]);
int int_rand(int const a, int const b);
void print_first_array(int, int,int array[MAX_SIZE][MAX_SIZE]);
void second_array(int, int, int array[MAX_SIZE][MAX_SIZE]);
int scan_integer();

int main()
{
    srand(time(NULL));
    int column_size;
    int line_size;
    int choice;
    int array[MAX_SIZE][MAX_SIZE];
    choice = init_choice(0);
    if (choice == 1) {
        column_size=init_random_column_size();
        line_size=init_random_line_size();
        printf("Array's size is [%d][%d]", column_size, line_size);
        init_random_array(column_size, line_size, array);
    }
    if (choice == 2) {
        column_size = init_manual_column_size();
        line_size = init_manual_line_size();
        init_manual_array(column_size, line_size, array);
    }
    print_first_array(column_size, line_size, array);
    second_array(column_size, line_size, array);
}

int int_rand(int const a, int const b)
{
    const double rand_0_1 = rand()/(RAND_MAX + 1.0);
    const int n = b - a + 1;
    const int res = a + (int) (rand_0_1 * n);
    return (res);
}


int init_choice()
{
    int choice;
    printf("Choose way to define an array's size: \n1 - Random \n2 - Manually\n");
    do {
        scanf("%d", &choice);
        if (choice !=1 && choice != 2) {
            printf("Your choice isn't correct. \nTry again: ");
        }
        getchar();
    } while ((choice !=1 && choice != 2));
    return(choice);
}

int init_random_column_size()
{
    int column_size = int_rand(0, MAX_SIZE);
    return(column_size);
}

int init_random_line_size()
{
    int line_size = int_rand(0, MAX_SIZE);
    return(line_size);
}

int init_manual_column_size()
{
    int column_size;
    do {
        printf("Enter size of array's column : (Max size is 8) \n");
        column_size = scan_integer();
        if (column_size > MAX_SIZE || column_size <= 0) {
            printf("Incorrect size. Try again \n");
        }
        getchar();
    } while (column_size > MAX_SIZE || column_size <= 0);
    return(column_size);
}

int init_manual_line_size()
{
    int line_size;
    do {
        printf("Enter size of array's line : (Max size is 8) \n");
        line_size = scan_integer();
        if (line_size > MAX_SIZE || line_size <= 0) {
            printf("Incorrect size. Try again \n");
        }
        getchar();
    } while (line_size > MAX_SIZE || line_size <= 0 );
    return(line_size);
}

void init_random_array(const int column_size,const int line_size,int array[MAX_SIZE][MAX_SIZE])
{
    printf("\nEnter min digit: ");
    int min_digit;
    min_digit = scan_integer();
    printf("\nEnter max digit: ");
    int max_digit;
    max_digit = scan_integer();

    for (int i = 0; i < column_size; i++)
    {
        for (int q = 0; q < line_size; q++)
        {
            array[i][q] = int_rand(min_digit , max_digit);
        }
    }
}

void init_manual_array(const int column_size,const int line_size,int array[MAX_SIZE][MAX_SIZE])
{
    for (int i = 0; i < column_size; i++)
    {
        for (int q = 0; q < line_size; q++)
        {
            int is_not_digit;
            printf("Enter element of array[%d][%d] \n",i,q);
            array[i][q] = scan_integer();
        }
    }
}

int scan_integer()
{
    int input_data;
    int is_integer;
    do
    {
        is_integer = (scanf("%d", &input_data));
        getchar();
        if (is_integer != 1) {
           printf("Isn't digit. Try again \n");
        }
    } while (is_integer != 1);
    return input_data;
}

void print_first_array(const int column_size,const int line_size,int array[MAX_SIZE][MAX_SIZE])
{
    for (int i = 0; i < column_size; i++) {
        for (int q = 0; q < line_size; q++) {
            printf("a[%d][%d] =  %d   ", i, q, array[i][q]);
        }
        printf("\n");
    }
}

void second_array(const int column_size,const int line_size,int array[MAX_SIZE][MAX_SIZE])
{
    int line_zero[MAX_SIZE];
    int column_zero[MAX_SIZE];
    for (int i = 0; i < column_size; i++) {
        for (int q = 0; q < line_size; q++)
        {
            if (array[i][q] == 0)
            {
               line_zero[i] = 1;
               column_zero[q] = 1;
            }
        }
    }
    printf("\n\nNew array: \n");
    for (int i = 0; i < column_size; i++)
    {
        for (int q = 0; q < line_size; q++)
        {
            if (line_zero[i] == 1 && column_zero[q] == 1)
            {
                array[i][q] = 0;
            }
            printf("a[%d][%d] =  %d   ", i, q, array[i][q]);
        }
        printf("\n");
    }
}
