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
            protected int num = 2;

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
            // Получает значение разрядности массива.
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
            // Возвращеет массив со значением введённых индексов (пользователь оперирует индексами начинающимися с 1 а не 0). 
            public int[] ReadIndex()
            {
                Console.WriteLine("Введите номер строки: ");
                int i = Int32.Parse(Console.ReadLine());
                
                while((i < 1) || (i > num))
                {
                    Console.WriteLine("Номер строки не быть меньше 1 и больше " + num + ". Повторите ввод: ");
                    i = Int32.Parse(Console.ReadLine());
                }

                Console.WriteLine("Введите номер элемента в строке: ");
                int j = Int32.Parse(Console.ReadLine());

                while ((j < 1) || (j > num))
                {
                    Console.WriteLine("Номер элемента в стоке  не быть меньше 1 и больше " + num + ". Повторите ввод: ");
                    j = Int32.Parse(Console.ReadLine());
                }

                Console.WriteLine("Введите [" + i + "," + j + "] элемент массива");
                int val = Int32.Parse(Console.ReadLine());

                while ((val != 0) && (val != 1))
                {
                    Console.WriteLine("Элемент может иметь только одно из значений (0 или 1). Повторите ввод: ");
                    val = Int32.Parse(Console.ReadLine());
                }

                massiv[i - 1, j - 1] = val;

                int [] index = new int[2] { i - 1, j - 1 };                
                return index;
            }


            // Проверяет элементы строки на равенство первому элементу в строке.
            // Получает индекс изменённой строки.
            // Возвращает ложь если значение любого элемента строки - не равно значению первого элемента этой строки.            
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
            // Получает индекс изменённого столбца.
            // Возвращает ложь если значение любого элемента столбца - не равно значению первого элемента этого столбца.            
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
            // Получает индексы введённого элемента.
            // Возвращает ложь если значение любого элемента диагоналей - не равно значению первого элемента этой диагонали.
            // Предварительно проверяется было ли изменение в первых элементах диагоналей. Т.к. одинаковое значение
            // во всех ячейках диагонали, которое используется по умолчанию - приводит к неверному результату проверки. 
            public bool ReviseDiag(int rows, int col)
            {
                if (rows != col)
                {
                    if (massiv[0, num - 1] == -1)
                        return false;

                    for (int i = 0; i < num; i++)
                    {                
                        if (massiv[i, num - (i + 1)] != massiv[0, num - 1])
                            return false;
                    }
                }
                else
                {
                    if (massiv[0, 0] == -1)
                        return false;

                    for (int i = 0; i < num; i++)
                    {                     
                        if ((massiv[i, i] != massiv[0, 0]))
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
            if(num < 2)
            {
                Console.WriteLine("Размерность матрицы не может быть менее 2. Устанавливаем значение по умолчанию равное 2. ");
                num = 2;
            }

            Matrix mat = new Matrix(num);
            mat.Print();            

            int[] index = new int[2];

            do
            {
                index = mat.ReadIndex();

                mat.Print();

            } while ((!mat.ReviseRows(index[0])) && (!mat.ReviseCol(index[1])) && (!mat.ReviseDiag(index[0], index[1])));

            Console.WriteLine("Программа успешно завершена.");
           

            Console.ReadKey();

        }
    }
}
