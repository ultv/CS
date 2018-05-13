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

                if (dignity == 10)
                {
                    Console.Write(" ___");
                    Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop + 1);
                    Console.Write("[   ]");
                    Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop + 1);
                    Console.Write("[ X ]");
                    Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop + 1);
                    Console.Write("[___]");
                }
                else
                {
                    Console.Write(" ___");
                    Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop + 1);
                    Console.Write("[   ]");
                    Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop + 1);
                    Console.Write("[ " + dignity + " ]");
                    Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop + 1);
                    Console.Write("[___]");
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
                    Card cd = new Card(rnd.Next(1, 11));
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

            // Принимает карту.
            public void TakenCard(Card cd)
            {                
                Enqueue(cd);
                Size++;
            }
        }

        class Gamer
        {
            protected List<Card> gamerCards = new List<Card>();
            protected int summ = 0;
            protected int victory = 0;

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

            public int Victory
            {
                get { return victory; }
                set { victory = value; }
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
                Console.WriteLine("\nПринимаю карту: ");                
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
                Console.WriteLine("\nСкидываю карту:");                
                ret.PrintDignity();
                gamerCards.RemoveAt(minIndex);

                return ret;
            }

            public void Info(string name)
            {
                Console.WriteLine("\nКарты " + name + ". " + "Сумма карт: " + this.Summ);

                foreach (Card cd in this.GamerCards)
                {
                    cd.PrintDignity();
                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop - 3);
                }
                Console.WriteLine("\n");
                Console.WriteLine("\n");
                //  Console.SetCursorPosition(Console.WindowLeft, Console.CursorTop + 2);

            }

        }
            

        class Round
        {

        }
       
        




        static void Main(string[] args)
        {
            Console.BufferHeight = 1000;

            bool fullVictory = false;

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

            //Dictionary<Gamer, string> allGamer = new Dictionary<Gamer, string>();
            Dictionary<string, Gamer> allGamer = new Dictionary<string, Gamer>();

            allGamer.Add("Alex", alex);
            allGamer.Add("Dmitry", dmitry);            

            while(!fullVictory)
            {
                // --------Раунд -------- //
                // Раздаём всем игрокам по 6 карт.
                for (int i = 0; i < 6; i++)
                {
                    foreach (Gamer gamer in allGamer.Values)
                    {
                        gamer.TakenCard(deck);
                    }
                }

                // Выводим карты игроков.
                foreach (Gamer gamer in allGamer.Values)
                {
                    string name = allGamer.FirstOrDefault(x => x.Value == gamer).Key;
                    gamer.Info(name);
                }

                foreach (Gamer gamer in allGamer.Values)
                {
                    for (int i = 0; i < gamer.Colution(); i++)
                    {
                        Console.WriteLine("\n" + allGamer.FirstOrDefault(x => x.Value == gamer).Key + ":");
                        deck.Enqueue(gamer.FindMinCard());
                        gamer.TakenAddCard(deck);
                    }
                }

                // Выводим карты игроков.
                foreach (Gamer gamer in allGamer.Values)
                {
                    string name = allGamer.FirstOrDefault(x => x.Value == gamer).Key;
                    gamer.Info(name);
                }

                // Выбираем победителя.
                string vic = "";
                int max = 0;
                foreach (Gamer gamer in allGamer.Values)
                {
                    if (gamer.Summ > max)
                    {
                        max = gamer.Summ;
                        vic = allGamer.FirstOrDefault(x => x.Value == gamer).Key;
                    }
                }

                Console.Write("\nРаунд выиграл - " + vic + "!");
                allGamer[vic].Victory++;

                // --------Раунд закончен ---------- //

                // Вернуть все карты в колоду
                for (int i = 5; i >= 0; i--)
                {
                    foreach (Gamer gamer in allGamer.Values)
                    {
                        deck.Enqueue(gamer.GamerCards[i]);
                        deck.Size++;
                        gamer.Summ = gamer.Summ - gamer.GamerCards[i].Dignity;
                        gamer.GamerCards.RemoveAt(i);
                    }
                }

                // Проверить общее количество побед.
                foreach (Gamer gamer in allGamer.Values)
                {
                    if (gamer.Victory == 5)
                    {
                        fullVictory = true;
                        Console.WriteLine("\nИгру выиграл - " + allGamer.FirstOrDefault(x => x.Value == gamer).Key + "!!!");
                    }                        
                }
            }
            



            Console.ReadKey();
        }
    }
}
