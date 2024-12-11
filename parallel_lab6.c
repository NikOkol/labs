#include <mpi.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h> // Добавлен для memcpy

// Функция для распределения блоков матриц
void distribute_matrix(double* matrix, double* local_block, int block_size, int n, MPI_Comm comm) {
    int coords[2];
    int rank;
    MPI_Comm_rank(comm, &rank);
    MPI_Cart_coords(comm, rank, 2, coords);

    int row_offset = coords[0] * block_size;
    int col_offset = coords[1] * block_size;

    for (int i = 0; i < block_size; i++) {
        for (int j = 0; j < block_size; j++) {
            local_block[i * block_size + j] = matrix[(row_offset + i) * n + (col_offset + j)];
        }
    }
}

// Функция для блочного умножения матриц
void multiply_blocks(double* A, double* B, double* C, int block_size) {
    for (int i = 0; i < block_size; i++) {
        for (int j = 0; j < block_size; j++) {
            for (int k = 0; k < block_size; k++) {
                C[i * block_size + j] += A[i * block_size + k] * B[k * block_size + j];
            }
        }
    }
}

int main(int argc, char* argv[]) {
    int rank, size;
    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    int n = 4; // Размер матрицы (NxN)
    if (argc > 1) n = atoi(argv[1]);

    int sqrt_size = sqrt(size);
    if (sqrt_size * sqrt_size != size) {
        if (rank == 0) printf("Number of processes must be a perfect square!\n");
        MPI_Abort(MPI_COMM_WORLD, 1);
    }

    if (n % sqrt_size != 0) {
        if (rank == 0) printf("Matrix size must be divisible by the square root of the number of processes!\n");
        MPI_Abort(MPI_COMM_WORLD, 1);
    }

    int block_size = n / sqrt_size;

    MPI_Comm grid_comm;
    int dims[2] = {sqrt_size, sqrt_size};
    int periods[2] = {1, 1}; // Циклический сдвиг
    MPI_Cart_create(MPI_COMM_WORLD, 2, dims, periods, 1, &grid_comm);

    double *A = NULL, *B = NULL, *C = NULL;
    if (rank == 0) {
        A = malloc(n * n * sizeof(double));
        B = malloc(n * n * sizeof(double));
        C = calloc(n * n, sizeof(double));

        // Инициализация матриц
        for (int i = 0; i < n * n; i++) {
            A[i] = rand() % 10;
            B[i] = rand() % 10;
        }
    }

    double* local_A = malloc(block_size * block_size * sizeof(double));
    double* local_B = malloc(block_size * block_size * sizeof(double));
    double* local_C = calloc(block_size * block_size, sizeof(double));
    double* temp_A = malloc(block_size * block_size * sizeof(double));

    distribute_matrix(A, local_A, block_size, n, grid_comm);
    distribute_matrix(B, local_B, block_size, n, grid_comm);

    for (int k = 0; k < sqrt_size; k++) {
        int coords[2];
        MPI_Cart_coords(grid_comm, rank, 2, coords);

        int source = (coords[0] + k) % sqrt_size;
        if (source == coords[1]) {
            memcpy(temp_A, local_A, block_size * block_size * sizeof(double));
        }

        MPI_Bcast(temp_A, block_size * block_size, MPI_DOUBLE, source, grid_comm);
        multiply_blocks(temp_A, local_B, local_C, block_size);
        MPI_Sendrecv_replace(local_B, block_size * block_size, MPI_DOUBLE, (coords[1] - 1 + sqrt_size) % sqrt_size, 0, (coords[1] + 1) % sqrt_size, 0, grid_comm, MPI_STATUS_IGNORE);
    }

    if (rank == 0) {
        free(A);
        free(B);
        free(C);
    }

    free(local_A);
    free(local_B);
    free(local_C);
    free(temp_A);

    MPI_Finalize();
    return 0;
}
