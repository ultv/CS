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
        
        // Осуществляет все операции над матрицей.
        class Matrix
        {            
            protected int[,] massiv;
            protected int num = 3;

            public int Num
            {
                get { return num; }
                set { num = value; }
            }

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

            // Принимает значение разрядности матрицы.
            public void SetNum()
            {
                Console.WriteLine("Введите количество элементов массива: ");
                num = Int32.Parse(Console.ReadLine());
            }

            // Выводит матрицу на экран.
            public void Print()
            {
                Console.WriteLine('\n' + "Массив:" + '\n');

                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        Console.Write(massiv[i, j] + "\t");
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

            // Читает с консоли индексы вводимого элемента.
            public int[] ReadIndex()
            {
                Console.WriteLine("Введите номер строки: ");
                int i = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Введите номер элемента в строке: ");
                int j = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Введите [" + i + "," + j + "] элемент массива");            
                massiv[i - 1,j - 1] = Int32.Parse(Console.ReadLine());

                int [] x = new int[2] { i - 1, j - 1 };
                return x;
            }


            // Проверяет элементы строки на равенство первому элементу в строке.
            public bool ReviseRows(int i)
            {
                for (int j = 0; j < num; j++)
                {
                    if (massiv[i, j] != massiv[i, 0])
                        return false;                    
                }
                
                return true;
            }

            // Проверяет элементы столбца на равенство первому элементу в столбце.
            public bool ReviseCol(int j)
            {
                for (int i = 0; i < num; i++)
                {
                    if (massiv[i, j] != massiv[0, j])
                        return false;
                }

                return true;
            }

            // Проверяет элементы диагоналей матрицы на равенство первым элементам в каждой диагонали.
            // Если индексы вводимого элемента не равны - побочная диагональ не изменялась,
            // соответственно проверяем только главную диагональ.
            public bool ReviseDiag(int rows, int col)
            {
                if (rows != col)
                {
                    for (int i = 0; i < num; i++)
                    {
                        if (massiv[i, num - (i + 1)] != massiv[0, num - 1])
                            return false;
                    }
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        if (massiv[i, i] != massiv[0, 0])
                            return false;
                    }

                }

                return true;
            }

        }




        // Присваивает значения элементам матрицы по введенным значениям индексов.
        // Завершает выполнение, если равны значения всех элементов строки, столбца или диагоналей.
        static void Main(string[] args)
        {
          
            Console.WriteLine("Введите количество элементов массива: ");
            int num = Int32.Parse(Console.ReadLine());

            Matrix mat = new Matrix(num);
            mat.Print();
            //mat.Read();

            int[] x = new int[2];

            do
            {
                x = mat.ReadIndex();

                mat.Print();

            } while ((!mat.ReviseRows(x[0])) && (!mat.ReviseCol(x[1])) && (!mat.ReviseDiag(x[0], x[1])));

            Console.WriteLine("Программа успешно завершена.");
           

            Console.ReadKey();

        }
    }
}
