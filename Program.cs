using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HomeWork1
{
    class Program
    {
        // Взаимодействует с пользователем в консоли.
        class User
        {
            protected int num = 3;

            public int Num
            {
                get { return num; }
                set { num = value; }
            }

            // Принимает значение разрядности матрицы.
            public void SetNum()
            {
                Console.WriteLine("Введите количество элементов массива: ");
                num = Int32.Parse(Console.ReadLine());
            }                        
        }

        
        // Осуществляет все операции над матрицей.
        class Matrix : User
        {            
            protected int[,] massiv;

            public Matrix()
            {               
                massiv = new int[num, num];

                for (int i = 0; i < num; i++)
                    for (int j = 0; j < num; j++)
                    {
                        massiv[i, j] = -1;
                    }
            }


            // Заполняет матрицу значением по умолчанию.
            public Matrix(int n)
            {
                num = n;
                massiv = new int[n, n];

                for (int i = 0; i < num; i++)
                    for (int j = 0; j < num; j++)
                    {
                        massiv[i, j] = -1;
                    }
            }

            // Выводит матрицу на экран.
            public void Print()
            {
                Console.WriteLine('\n' + "Массив:" + '\n');

                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        Console.Write(massiv[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine('\n');
            }

            // Заполняет матрицу введёнными значениями.
            public void Read()
            {
                for (int i = 0; i < num; i++)
                    for (int j = 0; j < num; j++)
                    {
                        Console.WriteLine("Введите [" + i + "," + j + "] элемент массива");
                        massiv[i, j] = Int32.Parse(Console.ReadLine());
                    }
            }

        }





        static void Main(string[] args)
        {

            User usr = new User();
            usr.SetNum();

            Matrix mat = new Matrix(usr.Num);
            mat.Print();
            mat.Read();
            mat.Print();






            Console.ReadKey();

        }
    }
}
