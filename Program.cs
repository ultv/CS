﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HomeWork1
{
    class Program
    {
        // Используется для передачи сведений в каком элементе или элементах матрицы одинаковые значения.
        enum Element { Rows, Col, MainDiag, SecDiag, RowsCol, RowsMainDiag, RowsSecDiag, ColMainDiag, ColSecDiag, DiagDiag }       

        // Выводит сообщения принятые в качестве параметра.
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

            // Заполняет матрицу при создании.
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

            // Выводит матрицу на экран c подсветкой заданных элементов матрицы.
            // Получает массив индексов элемента в пересечении с которым выявлены одинаковые элементы
            // и тип элемента для требуемого совершения действий.
            public void PrintColor(Element el, int[] index)
            {
                Console.WriteLine('\n' + "Массив:" + '\n');

                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        switch (el)
                        {
                            case Element.Rows:
                                if (i == index[0])
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.Col:
                                if (j == index[1])
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.MainDiag:
                                if (i == j)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.SecDiag:
                                if (j == num - i - 1)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.RowsCol:
                                if (i == index[0] || j == index[1])
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.RowsMainDiag:
                                if (i == index[0] || i == j)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.RowsSecDiag:
                                if (i == index[0] || (j == num - i - 1))
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.ColMainDiag:
                                if (j == index[1] || (i == j))
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.ColSecDiag:
                                if (j == index[1] || (j == num - i - 1))
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case Element.DiagDiag:
                                if (i == j || (j == num - i - 1))
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Gray;
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

            // Читает с консоли индексы вводимого элемента.
            // Возвращеет массив со значением введённых индексов (пользователь оперирует индексами начинающимися с 1 а не 0).
            // Обрабатывает исключения связанные с несоответствием типа введенных данных и выхода индексов за пределы массива.
            public int[] ReadIndex()
            {
                int i = 0;
                int j = 0;
                int val = -1;

                Console.WriteLine("Введите номер строки: ");                  

                while ((i < 1) || (i > num))
                {
                    try
                    {
                        i = Int32.Parse(Console.ReadLine());

                        if ((i < 1) || (i > num))
                                PrintErrorMessage("\nНомер должен быть числовым значением в диапазоне от 1 до " + num + ". Повторите ввод: ");
                    }
                    catch
                    {                 
                        PrintErrorMessage("\nНомер должен быть числовым значением в диапазоне от 1 до " + num + ". Повторите ввод: ");
                    }
                 
                }           

                Console.WriteLine("Введите номер элемента в строке: ");


                while ((j < 1) || (j > num))
                {                    
                    try
                    {
                        j = Int32.Parse(Console.ReadLine());

                        if ((j < 1) || (j > num))
                            PrintErrorMessage("\nНомер должен быть числовым значением в диапазоне от 1 до " + num + ". Повторите ввод: ");
                    }
                    catch
                    {
                        PrintErrorMessage("\nНомер должен быть числовым значением в диапазоне от 1 до " + num + ". Повторите ввод: ");
                    }
                }
                

                Console.WriteLine("Введите [" + i + "," + j + "] элемент массива");

                while ((val != 0) && (val != 1))
                {
                    try
                    {
                        val = Int32.Parse(Console.ReadLine());
                        if((val != 0) && (val != 1))
                            PrintErrorMessage("Элемент может иметь только одно из числовых значений (0 или 1)." +
                                "\nПовторите ввод для [" + i + "," + j + "] элемента массива: ");
                    }
                    catch
                    {
                        PrintErrorMessage("Элемент может иметь только одно из числовых значений (0 или 1)." +
                                "\nПовторите ввод для [" + i + "," + j + "] элемента массива: ");

                    }
                }

                massiv[i - 1, j - 1] = val;

                int[] index = new int[2] { i - 1, j - 1 };
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

            // Проверяет элементы главной диагонали матрицы на равенство первому элементу в диагонали.
            // Если индексы вводимого элемента не равны - диагональ не изменялась, можем пропустить проверку.
            // Получает индексы введённого элемента.
            // Возвращает ложь если значение любого элемента диагонали - не равно значению первого элемента диагонали.
            // Предварительно проверяется было ли изменение в первом элементе диагонали. Т.к. одинаковое значение
            // во всех ячейках диагонали, которое используется по умолчанию - приводит к неверному результату проверки. 
            public bool ReviseMainDiag(int rows, int col)
            {
                if (rows != col)
                    return false;
                else
                {
                    if (massiv[0, 0] == -1)
                        return false;

                    for (int i = 0; i < num; i++)
                    {
                        if ((massiv[i, i] != massiv[0, 0]))
                            return false;
                    }

                    return true;
                }
            }

            // Проверяет элементы побочной диагонали матрицы на равенство первому элементу в диагонали.            
            // Получает индексы введённого элемента.
            // Возвращает ложь если значение любого элемента диагонали - не равно значению первого элемента диагонали.
            // Предварительно проверяется было ли изменение в первом элементе диагонали. Т.к. одинаковое значение
            // во всех ячейках диагонали, которое используется по умолчанию - приводит к неверному результату проверки. 
            public bool ReviseSecDiag(int rows, int col)
            {
                if (massiv[0, num - 1] == -1)
                    return false;

                for (int i = 0; i < num; i++)
                {
                    if (massiv[i, num - (i + 1)] != massiv[0, num - 1])
                        return false;
                }

                return true;
            }
        }



        // По введённым индексам и значениям элементов присваивает значения элементам матрицы.
        // Проверяет равенство каждого нового элемента на равенство с элементами в столбцах, строках и диагоналях матрицы.
        // Если все элементы в строке, столбце или диагонали равны - передаёт значения индексов последнего элемента
        // и значение типа в котором сформировались элементы равные по значению для вывода на экран.
        // Строки, столбцы, диагонали (или их комбинация) в которых сформировались равные по значению элементы -
        // подсвечиваются другим цветом и выполнение программы завершается.        
        static void Main(string[] args)
        {
            int count = 0;
            bool exitRows = false;
            bool exitCol = false;
            bool exitMainDiag = false;
            bool exitSecDiag = false;

            Console.Title = "С# поток 7. Домашнее задание №1. Выполнил: Седов А.П.";
            Console.WriteLine("\nВведите количество элементов массива: ");

            while (count < 2)
            {
                try
                {
                    count = Int32.Parse(Console.ReadLine());
                    if (count < 2)
                    {
                        PrintErrorMessage("\nРазмерность матрицы должна иметь числовое значение не менее 2. Повторите ввод: ");                        
                    }
                }
                catch
                {
                    PrintErrorMessage("\nРазмерность матрицы должна иметь числовое значение не менее 2. Повторите ввод: ");                    
                }
                
            }
            

            Matrix mat = new Matrix(count);
            mat.Print();

            int[] index = new int[2];

            do
            {
                index = mat.ReadIndex();

                if (mat.ReviseRows(index[0]))
                {                  
                    exitRows = true;
                }
                if (mat.ReviseCol(index[1]))
                {               
                    exitCol = true;
                }
                if (mat.ReviseMainDiag(index[0], index[1]))
                {                  
                    exitMainDiag = true;
                }
                if (mat.ReviseSecDiag(index[0], index[1]))
                {                 
                    exitSecDiag = true;
                }
                
                mat.Print();

            } while (!exitRows && !exitCol && !exitMainDiag && !exitSecDiag);


            if (exitRows && exitCol)
            {
                mat.PrintColor(Element.RowsCol, index);
            }
            else if (exitRows && exitMainDiag)
            {
                mat.PrintColor(Element.RowsMainDiag, index);
            }
            else if (exitRows && exitSecDiag)
            {
                mat.PrintColor(Element.RowsSecDiag, index);
            }
            else if (exitCol && exitMainDiag)
            {
                mat.PrintColor(Element.ColMainDiag, index);
            }
            else if (exitCol && exitSecDiag)
            {
                mat.PrintColor(Element.ColSecDiag, index);
            }
            else if (exitMainDiag && exitSecDiag)
            {
                mat.PrintColor(Element.DiagDiag, index);
            }
            else if (exitRows)
            {
                mat.PrintColor(Element.Rows, index);
            }
            else if (exitCol)
            {
                mat.PrintColor(Element.Col, index);
            }
            else if (exitMainDiag)
            {
                mat.PrintColor(Element.MainDiag, index);
            }
            else if (exitSecDiag)
            {
                mat.PrintColor(Element.SecDiag, index);
            }
            else
                mat.Print();

            Console.WriteLine("Программа успешно завершена.");          
            Console.ReadKey();

        }
    }
}
