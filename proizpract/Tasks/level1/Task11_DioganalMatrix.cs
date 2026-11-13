using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proizpract.Tasks.level1
{
    [Common.TaskInfoAttribute("Level-1",11)]
    internal class Task11_DioganalMatrix
    {
        public string Title => "Диагональ матрицы N×N";

        public string Description => "найти сумму элементов главной диагонали матрицы N×N.";

        public string[] Requirements => new[] { "Размер N вводится пользователем (минимум 2).",
            "Матрица заполняется случайными числами.",
            "Сумма считается без LINQ, в одном цикле for",
            "Вывести саму матрицу и сумму."
        };

        public void Run()
        {
            int n = ReadMatrixSize();
            int[,] matrix = GenerateMatrix(n);

            Console.WriteLine("\nМатрица:");
            PrintMatrix(matrix, n);

            int sum = CalculateDiagonalSum(matrix, n);
            Console.WriteLine($"\nСумма главной диагонали: {sum}");
        }

        private int ReadMatrixSize()
        {
            while (true)
            {
                Console.Write("Введите размер матрицы N (минимум 2): ");
                if (int.TryParse(Console.ReadLine(), out int n) && n >= 2)
                {
                    return n;
                }
                Console.WriteLine("Ошибка: введите число >= 2");
            }
        }

        private int[,] GenerateMatrix(int n)
        {
            Random random = new Random();
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next(1, 100);
                }
            }
            return matrix;
        }

        private void PrintMatrix(int[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j],4}");
                }
                Console.WriteLine();
            }
        }

        private int CalculateDiagonalSum(int[,] matrix, int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += matrix[i, i];
            }
            return sum;
        }
    }
}
