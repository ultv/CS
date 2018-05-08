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
        enum Element {Rows, Col, MainDiag, SecDiag }

        public static void PrintErrorDimension()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nОчевидно вы ввели символное значение вместо числового.\n" +
            "В размерности матрицы будет использовано значение по умочанию равное 2.\n" +
            "Для смены значения закройте приложение и запустите ещё раз.\n" +
            "Используйте только числовые значения для работы с программой.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintErrorIndex()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nОчевидно вы ввели символное значение вместо числового.\n" +
            "Будет использовано значение индекса равное 1.\n" +
            "Для изменения значения элемента введите его индексы и значение ещё раз.\n" +
            "Используйте только числовые значения для работы с программой.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintErrorValue()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nОчевидно вы ввели символное значение вместо числового.\n" +
            "Значение элемента не будет изменено.\n" +
            "Для изменения значения элемента введите его индексы и значение ещё раз.\n" +
            "Используйте только числовые значения для работы с программой.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

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


            // Выводит матрицу на экран.
            public void PrintColorRows(int index)
            {
                Console.WriteLine('\n' + "Массив:" + '\n');

                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        if (j == index)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write(massiv[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine('\n');
            }


            // Выводит матрицу на экран c подсветкой заданных элементов матрицы.
            // Получает индекс строки с одинаковыми элементами
            // и тип элемента для совершения действий.
            public void PrintColor(Element el, int index)
            {
                
                Console.WriteLine('\n' + "Массив:" + '\n');

                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        switch (el)
                        {
                            case Element.Rows:
                                if (i == index)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.Col:
                                if (j == index)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.MainDiag:

                                break;
                            case Element.SecDiag:

                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                        }                                                

                        Console.Write(massiv[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Gray;
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
                int i = 1;
                int j = 1;
                int val = -1;

                Console.WriteLine("Введите номер строки: ");

                try
                {
                    i = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    PrintErrorIndex();
                    i = 1;
                }                
                
                while((i < 1) || (i > num))
                {                    
                    PrintErrorMessage("Номер строки не быть меньше 1 и больше " + num + ". Повторите ввод: ");

                    try
                    {
                        i = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        PrintErrorIndex();
                        i = 1;
                    }                    
                }

                Console.WriteLine("Введите номер элемента в строке: ");

                try
                {
                    j = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    PrintErrorIndex();
                    j = 1;
                }
               
                while ((j < 1) || (j > num))
                {                    
                    PrintErrorMessage("Номер элемента в стоке  не быть меньше 1 и больше " + num + ". Повторите ввод: ");
                 
                    try
                    {
                        j = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        PrintErrorIndex();
                        j = 1;
                    }                    
                }

                Console.WriteLine("Введите [" + i + "," + j + "] элемент массива");

                try
                {
                    val = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    PrintErrorValue();
                    val = -1;
                }
                
                
                    while ((val != 0) && (val != 1))
                    {

                    if (val != -1)                    
                        PrintErrorMessage("Элемент может иметь только одно из значений (0 или 1). Повторите ввод: ");
                    else
                        PrintErrorMessage("Повторите ввод значения для элемента [" + i + ", " + j + "]: ");

                    try
                        {
                            val = Int32.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            PrintErrorValue();
                            val = -1;
                        }
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
            // соответственно проверяем только главную диагональ. Получает индексы введённого элемента.
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
            int count = 2;

            Console.Title = "С# поток 7. Домашнее задание №1. Выполнил: Седов А.П.";
            Console.WriteLine("\nВведите количество элементов массива: ");

            try
            {
                count = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                PrintErrorDimension();
                count = 2;
            }
            
            if(count < 2)
            {                
                PrintErrorMessage("Размерность матрицы не может быть менее 2. Будет использовано значение по умолчанию равное 2. ");             
                count = 2;
            }

            Matrix mat = new Matrix(count);
            mat.Print();            

            int[] index = new int[2];

            do
            {
                index = mat.ReadIndex();

                // mat.Print();

                if (mat.ReviseRows(index[0]))
                    mat.PrintColor(Element.Rows, index[0]);
                else if (mat.ReviseCol(index[1]))
                    mat.PrintColor(Element.Col, index[1]);
                else mat.Print();

            } while ((!mat.ReviseRows(index[0])) && (!mat.ReviseCol(index[1])) && (!mat.ReviseDiag(index[0], index[1])));

            Console.WriteLine("Программа успешно завершена.");
           

            Console.ReadKey();

        }
    }
}
