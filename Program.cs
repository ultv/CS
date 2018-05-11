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

        class Card
        {
            protected int val = 0;
            protected bool inGame = false;

            public int Val { get; set; }
            public bool InGame { get; set; }

            // Выводит карту и значение.
            // Десять выводим как римскую.
            public void PrintVal(int v)
            {
                if(v == 10)
                {
                    Console.WriteLine(" ___");
                    Console.WriteLine("[   ]");
                    Console.WriteLine("[ X ]");
                    Console.WriteLine("[___]");
                }
                else
                {
                    Console.WriteLine(" ___");
                    Console.WriteLine("[   ]");
                    Console.WriteLine("[ " + v + " ]");
                    Console.WriteLine("[___]");
                }

            }

            public void PrintShirt()
            {
                Console.WriteLine(" ___");
                Console.WriteLine("[   ]");
                Console.WriteLine("[ ? ]");
                Console.WriteLine("[___]");
            }
            
        }




        static void Main(string[] args)
        {

            Card card1 = new Card();

            card1.PrintShirt();
            card1.PrintVal(1);
            card1.PrintVal(10);


            Console.ReadKey();
        }
    }
}
