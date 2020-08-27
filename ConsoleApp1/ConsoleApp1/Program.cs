﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {
        class Matrix
        {
            public int n { get; set; }
            public int[,] elements_2d { get; set; }
            public int[,,] elements_3d { get; set; }
            public string name { get; set; }
            public int onetime_assignment { get; set; }
            public int localrecurs_alg1 { get; set; }
            public int localrecurs_alg2 { get; set; }

            public Matrix(int N, string Name)
            {
                n = N;
                name = Name;
                elements_2d = new int[n, n];
                elements_3d = new int[n, n, n+1];
            }

            public void MatrixA_Construction()
            {
                bool forward;
                for (int i = 0; i < n; i++)
                {
                    forward = false;
                    elements_2d[i, 0] = i + n;
                    for (int j = 1; j < n; j++)
                    {
                        if (elements_2d[i, j-1] == n)
                            forward = true;
                        elements_2d[i, j] = (forward) ? elements_2d[i, j] = 0 : elements_2d[i, j - 1] - 1;
                    }
                }
            }

            public void MatrixB_Construction()
            {
                int avg = (n % 2 == 0) ? n / 2 : n / 2 + 1;
                Console.Write($"\nВиберiть як створити матрицю {name}\n" +
                "1 - випадковi елементи, 2 - ввести з клавiатури : ");
                string method = Console.ReadLine();
                switch (method)
                {
                    case "1":
                        Console.WriteLine($"\nГенерацiя випадкових чисел матрицi {name}...");
                        Random rand = new Random();
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                                elements_2d[i, j] = (n-1-j <= i & i <=j) ? rand.Next() : 0;
                        }

                        Console.WriteLine("Готово!");
                        break;
                    case "2":
                        Console.WriteLine($"\nВведення чисел з клавiатури...");
                        for (int i = 0; i < n; i++)
                        {
                            Console.WriteLine("");
                            for (int j = 0; j < n; j++)
                            {
                                if (n - 1 - j <= i & i <= j)
                                {
                                    Console.Write($"{name}[{i}{j}] = ");
                                    try { elements_2d[i, j] = Convert.ToInt32(Console.ReadLine()); }
                                    catch { Console.WriteLine("Помилка. повторiть, будь-ласка!"); j--; }
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Помилка...Натисніть 1 або 2, будь-ласка...");
                        this.MatrixB_Construction();
                        break;
                }
            }

            public void ShowMatrix(bool size_3d = false)
            {
                Console.WriteLine("Матриця " + name);
                if (n > 7)
                    Console.WriteLine("Надто великий розмiр матрицi!");
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write("[");
                        for (int j = 0; j < n; j++)
                        {
                            if (size_3d)
                                Console.Write("{0,-18}", elements_3d[i, j, n]);
                            else
                                Console.Write("{0,-18}", elements_2d[i, j]);
                        }
                        Console.Write("]\n");
                    }
                }
            }

            public int OneTimeAssignment(Matrix A, Matrix B)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        elements_3d[i, j, 0] = 0;
                        for (int k = 0; k < n; k++)
                        {
                            elements_3d[i, j, k + 1] = elements_3d[i, j, k] + A.elements_2d[i, k] * B.elements_2d[k, j];
                            onetime_assignment++;
                            onetime_assignment++;
                        }
                    }
                }
                return onetime_assignment;
            }

            public int LocallyRecursiveAlgorithm1(Matrix A, Matrix B, int i, int j, int k)
            {
                if (i < n & j < n & k < n)
                {
                    if (A.elements_2d[i, k] != 0 & B.elements_2d[k, j] != 0)
                    {
                        elements_3d[i, j, n] += A.elements_2d[i, k] * B.elements_2d[k, j];
                        localrecurs_alg1++;
                        localrecurs_alg1++;
                    }

                    k = (k == n-1) ? 0 : k+1;
                    j = (k == 0 & j == n - 1) ? 0 : ((k == 0) ? j + 1 : j);
                    i = (k == 0 & j == 0) ? i + 1 : i;
                    this.LocallyRecursiveAlgorithm1(A, B, i, j, k);                    
                    
                }
                return localrecurs_alg1;
            }

            public int LocallyRecursiveAlgorithm2(Matrix A, Matrix B)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        A.elements_3d[i, j, 0] = A.elements_2d[i, j];
                        B.elements_3d[i, j, 0] = B.elements_2d[i, j];
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            A.elements_3d[i, k, j + 1] = A.elements_3d[i, k, j];
                            B.elements_3d[k, j, i + 1] = B.elements_3d[k, j, i];
                            if (A.elements_3d[i, k, 0] != 0 & B.elements_3d[k, j, 0] != 0)
                            {
                                elements_3d[i, j, k + 1] = elements_3d[i, j, k] + A.elements_3d[i, k, j] * B.elements_3d[k, j, i];
                                localrecurs_alg2++;
                                localrecurs_alg2++;
                            }
                            else
                            {
                                elements_3d[i, j, k + 1] = elements_3d[i, j, k];
                            }
                        }
                    }
                }
                return localrecurs_alg2;
            }
        }

        static void Main(string[] args)
        {
            int n = 0;
            while (true)
            {
                Console.WriteLine("Введiть розмiр матрицi : ");
                try { n = int.Parse(Console.ReadLine()); break;}
                catch { Console.WriteLine("Incorrect input, re-enter, please!"); }
            }

            Matrix A = new Matrix(n, "A");
            A.MatrixA_Construction();
            A.ShowMatrix();

            Matrix B = new Matrix(n, "B");
            B.MatrixB_Construction();
            B.ShowMatrix();

            Console.WriteLine("\nАлгоритм з одноразовим присвоєнням");
            Matrix C1 = new Matrix(n, "C1");
            int count1 = C1.OneTimeAssignment(A, B);
            C1.ShowMatrix(true);
            Console.WriteLine($"Кiлькiсть операцiй : {count1}");

            Console.WriteLine("\nЛокально-рекурсивний алгортм");
            Matrix C2 = new Matrix(n, "C2");
            int count2 = C2.LocallyRecursiveAlgorithm1(A, B, 0, 0, 0);
            C2.ShowMatrix(true);
            Console.WriteLine($"Кiлькiсть операцiй : {count2}");

            Console.ReadKey();
        }
    }
}
