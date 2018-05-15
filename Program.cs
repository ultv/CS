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
        
        // Содержит "номинал" карты.
        // Выводит изображение карты в консоль.
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

        // Заполняет колоду картами.
        // Выводит колоду в консоль.
        // Получает карту сброшенную игроком.
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

        // Содержит коллекцию карт, сумму своих карт, количество своих побед.
        // Получает карты из колоды.
        // Решает скидывать ли карты и их количество.
        // Если решил скидывать - отдаёт карту с минимальным значением,
        // вместо неё из колоды принимает другую.        
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
                Console.WriteLine("Принимаю карту: ");                
                deck.Peek().PrintDignity();
                Console.WriteLine("\n");
                gamerCards.Add(deck.Dequeue());
                deck.Size--;
            }

            // Возвращает количество скидываемых карт.
            // Если 0 - карты не скидывает.
            public int Colution()
            {
                Random rnd = new Random();

                return rnd.Next(0, 3);
            }

            // Возвращает минимальную карту игрока.           
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
                Console.WriteLine("Скидываю карту:");                
                ret.PrintDignity();
                Console.WriteLine("\n");
                gamerCards.RemoveAt(minIndex);

                return ret;
            }

            // Выводит все свои карты и сумму.
            public void Info(string name)
            {
                Console.WriteLine("-----------------------------------------------\n");
                //Console.WriteLine("Карты игрока " + name + ". " + "Сумма карт: " + this.Summ);
                Console.WriteLine($"Карты игрока {name}. Сумма карт: {this.Summ}");

                foreach (Card cd in this.GamerCards)
                {
                    cd.PrintDignity();
                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop - 3);
                }
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\n-----------------------------------------------\n");
            }
        }


        class Croupier
        {
            public int NumRaund { get; set; }
        //    protected CardDeck deck = new CardDeck();

            // Обрабатывает введённое пользователем количество игроков.
            // Количество ограничиваем по принципу, что нельзя играть одному и всем должно хватить карт.
            // Возвращает количестов игроков.
            public int GetNumGamer()
            {
                int numGamer = 0;
                                
                while ((numGamer < 2) || (numGamer > 4))
                {
                    Console.WriteLine("\nВведите количестов игроков (от 2-х до 4-х):\n");
                    try
                    {
                        numGamer = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("\nИспользуйте только числовое значение.");
                    }
                }

                return numGamer;
            }

            // Обрабатывает введённые пользователем имена игроков и заносит в коллекцию.
            // Принимает коллекцию игроков и их количество.
            public void RegisterGamer(Dictionary<string, Gamer> allGamer, int numGamer)
            {
                for (int i = 1; i <= numGamer; i++)
                {
                    bool ok = false;

                    while (!ok)
                    {
                        ok = true;
                        Console.WriteLine($"\nВведите имя {i} игрока:\n");
                        string name = Console.ReadLine();

                        if ((name != " ") && (!name.Contains(" ")))
                        {
                            Gamer gm = new Gamer();
                            try
                            {
                                allGamer.Add(name, gm);
                            }
                            catch
                            {
                                Console.WriteLine("\nИгрок с таким именем уже существует!");
                                ok = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nИспользовать пустое имя или пробел в имени недопустимо.");
                            ok = false;
                        }
                    }
                }
            }

            // Проверить общее количество побед.
            // Возвращает истину в случае 5-ти кратной побыды игрока.
            public bool CheсkVictory(Dictionary<string, Gamer> allGamer)
            {                
                foreach (Gamer gamer in allGamer.Values)
                {
                    if (gamer.Victory == 5)
                    {                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Игру выиграл - {allGamer.FirstOrDefault(x => x.Value == gamer).Key} !!!");
                        return true;
                    }
                }

                Console.WriteLine("Нажмите любую клавишу для игры в следующем раунде.");

                return false;
            }

            // Раздаёт всем игрокам по 6 карт.
            public void GiveCards (Dictionary<string, Gamer> allGamer, CardDeck deck)
            {
                
                for (int i = 0; i < 6; i++)
                {
                    foreach (Gamer gamer in allGamer.Values)
                    {
                        gamer.TakenCard(deck);
                    }
                }
            }

            // Помогает игрокам пожелавшим скинуть карты .
            public bool WillReturnCards(Dictionary<string, Gamer> allGamer, CardDeck deck)
            {
                bool solution = false;
                
                foreach (Gamer gamer in allGamer.Values)
                {                    

                    for (int i = 0; i < gamer.Colution(); i++)
                    {
                        Console.WriteLine(allGamer.FirstOrDefault(x => x.Value == gamer).Key + ":\n");
                        deck.Enqueue(gamer.FindMinCard());
                        gamer.TakenAddCard(deck);
                        solution = true;
                    }
                }

                if (solution)
                    return true;
                else return false;
            }

            // Выбирает победителя раунда.
            public void SelectRoundVictory(Dictionary<string, Gamer> allGamer)
            {                
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

                Console.Write($"{NumRaund} раунд выиграл - {vic} !\n\n");
                allGamer[vic].Victory++;
            }

            // Показывает счет игры.
            public void ShowCount(Dictionary<string, Gamer> allGamer)
            {
                Console.Write("Счет: / ");
                foreach (Gamer gamer in allGamer.Values)
                {
                    Console.Write(allGamer.FirstOrDefault(x => x.Value == gamer).Key + " - " + gamer.Victory + " / ");
                }
                Console.Write("\n\n");
            }

            // Собирает карты у игроков и возвращает в колоду. 
            public void CollectCards(Dictionary<string, Gamer> allGamer, CardDeck deck)
            {                
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
            }

            // Выводит карты игроков.
            public void ShowCards(Dictionary<string, Gamer> allGamer)
            {                
                foreach (Gamer gamer in allGamer.Values)
                {
                    string name = allGamer.FirstOrDefault(x => x.Value == gamer).Key;
                    gamer.Info(name);
                }
            }

        }        


        // Создает: крупье, колоду, коллекцию игроков.
        // Регистрирует игроков.
        // Начало раунда - нумерует раунд, раздает карты.
        // Показывает карты игроков, если карты менялись показывает ещё раз.
        // Выбирает победителя в раунде.
        // Показывает счёт игры, собирает карты.
        // Если есть 5-ти кратный победитель - объевляет и заканчивает игру,
        // если нет - возвращается к началу раунда.
        static void Main(string[] args)
        {

            // Console.BufferHeight = 2000;
            Console.WindowHeight = 72; //82; 
                
            Croupier boss = new Croupier();
            CardDeck deck = new CardDeck();            
            Dictionary<string, Gamer> allGamer = new Dictionary<string, Gamer>();

            boss.RegisterGamer(allGamer, boss.GetNumGamer());                 

            while (true)
            {

                Console.Clear();
                            
                boss.NumRaund++;
                boss.GiveCards(allGamer, deck);

                Console.WriteLine($"\nСейчас в колоде: {deck.Size} карты.\n");

                boss.ShowCards(allGamer);
                
                // Если игроки поменяли карты - выводим карты игроков ещё раз.
                if (boss.WillReturnCards(allGamer, deck))
                {
                    boss.ShowCards(allGamer);                 
                }
                else
                {
                    Console.WriteLine("Игроки не стали менять карты.\n");
                }    

                boss.SelectRoundVictory(allGamer);
                boss.ShowCount(allGamer);
                boss.CollectCards(allGamer, deck);
               
                if (boss.CheсkVictory(allGamer))
                    break;

                Console.ReadKey();         
            }

           Console.ReadKey();
        }
    }
}
