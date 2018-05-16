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
            public int Dignity { get; set; }

            public Card() { }

            public Card(int dign)
            {
                Dignity = dign;
            }

            // Выводит карту и значение.
            // Десять выводим как римскую.
            public virtual void PrintDignity()
            {
                if (Dignity == 10)
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
                    Console.Write("[ " + Dignity + " ]");
                    Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop + 1);
                    Console.Write("[___]");
                }
            }

            // Выводит рубашку карты.
            public virtual void PrintShirt()
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
        class CardDeck : Card
        {

            protected int maxSize = 36;
            public int Size { get; set; }
            public Queue<Card> Deck { get; set; }

            // Заполняет созданную колоду картами.
            // Значения картам - присваиваются случайным образом, в диапазоне от 1 до 10.
            public CardDeck()
            {
                Deck = new Queue<Card>();

                Random rnd = new Random();

                for (int i = 0; i < maxSize; i++)
                {
                    Card cd = new Card(rnd.Next(1, 11));
                    Deck.Enqueue(cd);
                    Size++;
                }
            }

            // Выводит колоду с картами.
            public override void PrintDignity()
            {
                foreach (Card cd in Deck)
                {
                    cd.PrintDignity();
                }
            }

            // Выводит "рубашку" колоды с количеством карт в колоде.
            public override void PrintShirt()
            {
                Console.Write("В колоде:");
                Console.SetCursorPosition(Console.CursorLeft - 9, Console.CursorTop + 1);
                Console.Write(" ___");
                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop + 1);
                Console.Write("[   ]]]");
                Console.SetCursorPosition(Console.CursorLeft - 7, Console.CursorTop + 1);
                Console.Write("[ " + Size + " ]]");
                Console.SetCursorPosition(Console.CursorLeft - 7, Console.CursorTop + 1);
                Console.Write("[карт]]");
            }

            // Принимает карту.
            public void TakenCard(Card cd)
            {
                Deck.Enqueue(cd);
                Size++;
            }
        }



        // Содержит коллекцию карт, сумму своих карт, количество своих побед.
        // Получает карты из колоды.
        // Решает скидывать ли карты и их количество.
        // Если решил скидывать - отдаёт карту с минимальным значением,
        // вместо неё из колоды принимает другую.        
        class Gamer //: Croupier
        {    

            public int Summ { get; set; }
            public int Victory { get; set; }
            public List<Card> GamerCards { get; set; }

            public Gamer( )
            {
                GamerCards = new List<Card>();
            }

            // Принимает при раздаче карту из колоды.
            public void TakenCard(CardDeck deck)
            {
                Summ = Summ + deck.Deck.Peek().Dignity;              
                GamerCards.Add(deck.Deck.Dequeue());
                deck.Size--;                    
            }

            // Возвращает минимальную карту игрока.           
            public Card FindMinCard(Croupier boss)
            {
                int min = GamerCards[0].Dignity;
                int minIndex = 0;
                int index = 0;

                foreach (Card cd in GamerCards)
                {
                    if (min > cd.Dignity)
                    {
                        min = cd.Dignity;
                        minIndex = index;
                    }

                    index++;
                }

                Card ret = new Card(0);
                ret = GamerCards[minIndex];
                Summ = Summ - ret.Dignity;
                Console.WriteLine("Скидываю:");
                ret.PrintDignity();         
                GamerCards.RemoveAt(minIndex);

                return ret;
            }

            // Принимает дополнительно первую карту из колоды.
            public void TakenAddCard(CardDeck deck, Croupier boss)
            {
                Summ = Summ + deck.Deck.Peek().Dignity;
                Console.WriteLine("Принимаю: ");
                Console.SetCursorPosition(Console.CursorLeft + 26, Console.CursorTop + 0);
                deck.Deck.Peek().PrintDignity();
                GamerCards.Add(deck.Deck.Dequeue());
                deck.Size--;
                Console.SetCursorPosition(Console.CursorLeft + 7, Console.CursorTop - 4);
                boss.Deck.PrintShirt();           
                Console.WriteLine("\n");
                
            }

            // Возвращает количество скидываемых карт.
            // Если 0 - карты не скидывает.
            public int Colution()
            {
                Random rnd = new Random();

                return rnd.Next(0, 3);
            }            

            // Выводит все свои карты и сумму.
            public void Info(string name)
            {
                Console.WriteLine("\n-----------------------------------------------\n");
                //Console.WriteLine("Карты игрока " + name + ". " + "Сумма карт: " + this.Summ);
                Console.WriteLine($"Карты игрока {name}. Сумма карт: {this.Summ}");

                foreach (Card cd in this.GamerCards)
                {
                    cd.PrintDignity();
                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop - 3);
                }
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\n-----------------------------------------------");
            }
        }




        // При создании - "открывает колоду"
        // и "бланк списка игроков".
        class Croupier
        {
            
            public int NumRaund { get; set; }
            public CardDeck Deck { get; set; }
            public Dictionary<string, Gamer> AllGamer { get; set; }
            

            public Croupier()
            {
                Deck = new CardDeck();
                AllGamer = new Dictionary<string, Gamer>();
            }

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
            // Принимает количество игроков.
            public void RegisterGamer(int numGamer)
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
                                AllGamer.Add(name, gm);
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

            // Проверяет общее количество побед.
            // Возвращает истину в случае 5-ти кратной побыды игрока.
            public bool CheсkVictory( )
            {                
                foreach (Gamer gamer in AllGamer.Values)
                {
                    if (gamer.Victory == 5)
                    {                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Игру выиграл - {AllGamer.FirstOrDefault(x => x.Value == gamer).Key} !!!");
                        return true;
                    }
                }

                Console.WriteLine("Нажмите любую клавишу для игры в следующем раунде.");

                return false;
            }

            // Раздаёт всем игрокам по 6 карт.
            public void GiveCards( )
            {
                
                for (int i = 0; i < 6; i++)
                {
                    foreach (Gamer gamer in AllGamer.Values)
                    {
                        gamer.TakenCard(Deck);
                    }
                }
            }

            // Помогает игрокам скинуть карты .
            public bool WillReturnCards(Croupier boss)
            {
                bool solution = false;
                
                foreach (Gamer gamer in AllGamer.Values)
                {                    

                    for (int i = 0; i < gamer.Colution(); i++)
                    {
                        Console.WriteLine($"{AllGamer.FirstOrDefault(x => x.Value == gamer).Key}:\n");
                        Deck.Deck.Enqueue(gamer.FindMinCard(boss));                                                
                        Deck.Size++;
                        Console.SetCursorPosition(Console.CursorLeft + 7, Console.CursorTop - 4);
                        boss.Deck.PrintShirt();
                        Console.SetCursorPosition(Console.CursorLeft + 7, Console.CursorTop - 4);
                        solution = true;
                        gamer.TakenAddCard(Deck, boss);
                    }
                }

                if (solution)
                    return true;
                else return false;
            }

            // Выбирает победителя раунда.
            public void SelectRoundVictory( )
            {                
                string vic = "";
                int max = 0;
                foreach (Gamer gamer in AllGamer.Values)
                {
                    if (gamer.Summ > max)
                    {
                        max = gamer.Summ;
                        vic = AllGamer.FirstOrDefault(x => x.Value == gamer).Key;
                    }
                }

                Console.Write($"\n{NumRaund} раунд выиграл - {vic} !\n\n");
                AllGamer[vic].Victory++;
            }

            // Показывает счет игры.
            public void ShowCount( )
            {
                Console.Write("Счет: / ");
                foreach (Gamer gamer in AllGamer.Values)
                {
                    Console.Write(AllGamer.FirstOrDefault(x => x.Value == gamer).Key + " - " + gamer.Victory + " / ");
                }
                Console.Write("\n\n");
            }

            // Собирает карты у игроков и возвращает в колоду. 
            public void CollectCards( )
            {                
                for (int i = 5; i >= 0; i--)
                {
                    foreach (Gamer gamer in AllGamer.Values)
                    {
                        Deck.Deck.Enqueue(gamer.GamerCards[i]);
                        Deck.Size++;
                        gamer.Summ = gamer.Summ - gamer.GamerCards[i].Dignity;
                        gamer.GamerCards.RemoveAt(i);
                    }
                }
            }

            // Выводит карты игроков.
            public void ShowCards( )
            {                
                foreach (Gamer gamer in AllGamer.Values)
                {
                    string name = AllGamer.FirstOrDefault(x => x.Value == gamer).Key;
                    gamer.Info(name);
                }
            }

        }        


        // Создает крупье, регистрирует игроков.
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

            boss.RegisterGamer(boss.GetNumGamer());                 

            while (true)
            {

                Console.Clear();
                Console.WriteLine();
                boss.NumRaund++;
                boss.Deck.PrintShirt();
                Console.WriteLine();
                boss.GiveCards();                
                boss.ShowCards();
                Console.WriteLine();
                boss.Deck.PrintShirt();
                Console.WriteLine("\n");

                // Если игроки поменяли карты - выводим карты игроков ещё раз.
                if (boss.WillReturnCards(boss))
                {
                    boss.ShowCards();                 
                }
                else
                {
                    Console.WriteLine("Игроки не стали менять карты.");
                }    

                boss.SelectRoundVictory();
                boss.ShowCount();
                boss.CollectCards();
               
                if (boss.CheсkVictory())
                    break;

                Console.ReadKey();         
            }

           Console.ReadKey();
        }
    }
}
