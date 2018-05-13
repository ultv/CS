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
            protected int size = 0;
            protected int maxSize = 36;
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

                for (int i = 0; i < maxSize; i++)
                {
                    Card cd = new Card(rnd.Next(1, 10));
                    this.Enqueue(cd);
                    size++;
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
        }

        class Gamer
        {
            protected List<Card> gamerCards = new List<Card>();
            protected int summ = 0;

            public int Summ
            {
                get { return summ; }
                set { summ = value; }
            }

            public List<Card> GamerCards
            {
                get { return gamerCards; }
                set { gamerCards = value;  }
            }
        
            // Принимает при раздаче первую карту из колоды.
            public void TakenCard(CardDeck deck)
            {
                summ = summ + deck.Peek().Dignity;              
                gamerCards.Add(deck.Dequeue());
                deck.Size--;                    
            }

            // Принимает дополнительно первую карту из колоды.
            public void TakenAddCard(CardDeck deck)
            {
                summ = summ + deck.Peek().Dignity;
                Console.WriteLine("\nПринимаю карту - ");
                deck.Peek().PrintDignity();
                gamerCards.Add(deck.Dequeue());
                deck.Size--;
            }

            // Возвращает количество скидываемых карт.
            public int Colution()
            {
                Random rnd = new Random();

                return rnd.Next(0, 3);
            }

            // Возвращает минимальную карту игрока.
            /// Можно просто sort() - не стработал.
            public Card FindMinCard()
            {
                int min = gamerCards[0].Dignity;
                int minIndex = 0;
                int index = 0;

                foreach (Card cd in gamerCards)
                {
                    if (min > cd.Dignity)
                    {
                        min = cd.Dignity;
                        minIndex = index; 
                    }

                    index++;
                }

                Card ret = new Card(0);
                ret = gamerCards[minIndex];
                summ = summ - ret.Dignity;
                Console.WriteLine("\nСкидываю карту -");
                ret.PrintDignity();
                gamerCards.RemoveAt(minIndex);

                return ret;
            }

            public void Info(string name)
            {
                Console.WriteLine("\nКарты " + name + ": "); // + this.Values.ToString());
                foreach (Card cd in this.GamerCards)
                {
                    cd.PrintDignity();
                }
                Console.WriteLine("\nСумма карт " + name + ": " + this.Summ);
            }



        }
            

        class Round
        {

        }
       
        




        static void Main(string[] args)
        {
    
            CardDeck deck = new CardDeck();
            Console.WriteLine("\nВ колоде: " + deck.Size + "карт.");

            /*
            foreach (Card cd in deck)
            {
                deck.Print();
            }
            */

            Gamer alex = new Gamer();
            Gamer dmitry = new Gamer();

            Dictionary<Gamer, string> allGamer = new Dictionary<Gamer, string>();

            allGamer.Add(alex, "Alex");
            allGamer.Add(dmitry, "Dmitry");

            // Раздаём всем игрокам по 6 карт.
            for (int i = 0; i < 6; i++)
            {
                foreach(Gamer gamer in allGamer.Keys)
                {
                    gamer.TakenCard(deck);
                }                
            }

            // Выводим карты игроков
            for (int i = 0; i < 6; i++)
            {
                foreach (Gamer gamer in allGamer.Keys)
                {
                    string name = allGamer[gamer];
                    gamer.Info(name);
                }
            }


            //alex.Info();
            //dmitry.Info();

            for (int i = 0; i < alex.Colution(); i++)
            {
                deck.Enqueue(alex.FindMinCard());
                alex.TakenAddCard(deck);
            }

          //  alex.Info();

            for (int i = 0; i < dmitry.Colution(); i++)
            {
                deck.Enqueue(dmitry.FindMinCard());
                dmitry.TakenAddCard(deck);
            }

         //   dmitry.Info();

            Console.ReadKey();
        }
    }
}
