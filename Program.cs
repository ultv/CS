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

        class Card
        {
            protected int dignity = 0;
            protected bool inGame = false;

            public Card(int dign)
            {
                dignity = dign;
            }

            public int Dignity
            {
                get { return dignity; }
                set { dignity = value; }
            }
            public bool InGame
            {
                get { return inGame; }
                set { inGame = value; }
            }

            // Выводит карту и значение.
            // Десять выводим как римскую.
            public void PrintDignity()
            {
                if(dignity == 10)
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
                    Console.WriteLine("[ " + dignity + " ]");
                    Console.WriteLine("[___]");
                }

            }

            // Выводит рубашку карты.
            public void PrintShirt()
            {
                Console.WriteLine(" ___");
                Console.WriteLine("[   ]");
                Console.WriteLine("[ ? ]");
                Console.WriteLine("[___]");
            }
            
        }

        class CardDeck : Queue<Card>
        {
            protected int size = 36;
            public int Size
            {
                get { return size; }
                set { size = value; }
            }

            // Заполняет созданную колоду картами.
            // Значения картам - присваиваются случайным образом, в диапазоне от 1 до 10.
            public CardDeck()
            {
                Random rnd = new Random();

                for (int i = 0; i < 36; i++)
                {
                    Card cd = new Card(rnd.Next(1,10));
                    this.Enqueue(cd);          
                }
            }

            // Выводит колоду с картами.
            public void Print()
            {
                foreach (Card cd in this)
                {
                    cd.PrintDignity();
                }
            }

            class Gamer : Dictionary<int, string>
            {
                protected List<Card> gamerCards;
                protected int summ = 0;

                public int Summ
                {
                    get { return summ; }
                    set { summ = value; }
                }
        
                // Принимает карту.
                public void TakenCard(Card card)
                {
                    gamerCards.Add(card);
                    summ = summ + card.Dignity;
                }

                // Скидывает карту обратно.
                public Card ThrowCard()
                {
                    return gamerCards[0];
                }



            }        
       
        }




        static void Main(string[] args)
        {
    
            CardDeck deck = new CardDeck();
     
            for (int i = 0; i < deck.Size; i++)
            {
                deck.Print();
            }


            Console.ReadKey();
        }
    }
}
