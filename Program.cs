using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace HomeWork1
{
 
    class Program
    {
        static string delimiter = "--------------------";
        static int positionContent = 15;

        // Счёт клиента.
        class Account
        {

        }

        // Данные клиента.
        class Client
        {

        }

        // Операция со счетами.
        class Transaction
        {

        }

        // Данные банка.
        [DataContract]
        class Bank
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int IDBank { get; set; }
            [DataMember]
            public string Sity { get; set; }
            [DataMember]
            public string Director { get; set; }
            [DataMember]
            public List<Client> Clients = new List<Client>();
            [DataMember]
            public DateTime DataOpen { get; set; }

            public Bank(string name, int id, string sity, string director)
            {
                Name = name;
                IDBank = id;
                Sity = sity;
                Director = director;
                DataOpen = DateTime.Now;                

                Console.WriteLine($"\n{delimiter} {DataOpen.ToLongDateString()} {delimiter}");
                Console.WriteLine($"\nОткрыто отделение {Name}а №{IDBank} в городе {Sity}.");

                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Bank));

                using (FileStream fs = new FileStream("Banks.json", FileMode.Append))
                {
                    jsonFormatter.WriteObject(fs, this);                    
                }

            }
        }

        // Данные о всех банках.
        class ControlBank
        {
            public List<Bank> Banks = new List<Bank>();

            public ControlBank()
            {

                Console.WriteLine("\nСоздана система управления банками.");
            }

            // Регистрирует новый банк.
            public void RegBank(Bank bank)
            {
                Banks.Add(bank);
                Console.WriteLine($"\n{delimiter} {bank.DataOpen.ToLongDateString()} {delimiter}");
                Console.WriteLine($"\n{bank.Name} зарегистрирован в системе управлениеями банками.");
            }

            public void ShowBanks()
            {
                Console.Clear();
                Console.SetCursorPosition(0, positionContent);
                Console.WriteLine($"\nВ системе управления банками зарегистрированы:");

                foreach (Bank bank in Banks)
                {                    
                    Console.WriteLine($"\n{bank.Name}");
                }
            }
        }

        // 
        class Menu
        {
            
            protected bool exit = false;
            bool correctIn = false;
            int choice = -1;

            public Menu(ControlBank dep)
            {                

                while (!exit)
                {                    
                    Console.SetCursorPosition(0, 0);                    
                    Console.WriteLine("\nДоступные действия:");
                    Console.WriteLine("\n1 - Показать банки.");
                    Console.WriteLine("\n2 - Показать клиентов.");
                    Console.WriteLine("\n3 - Зарегистрировать клиента.");
                    Console.WriteLine("\n0 - Выход.");
                    Console.WriteLine($"\n{delimiter}\n");

                    while (!correctIn)
                    {
                        try
                        {
                            choice = Int32.Parse(Console.ReadLine());

                            if ((choice < 0) || (choice > 3))
                            {
                                correctIn = false;
                                Console.WriteLine("\nНеверное значение. Повторите ввод:\n");
                            }
                            else correctIn = true;
                        }
                        catch
                        {
                            correctIn = false;
                            Console.WriteLine("\nНеверное значение. Повторите ввод:\n");
                        }
                                               
                    }

                    correctIn = false;                                        

                    switch(choice )
                    {
                        case 1:
                            dep.ShowBanks();
                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                        case 0:
                            exit = true;
                            break;
                    }
                }
            }
        }



        static void Main(string[] args)
        {
            Console.Clear();
            Console.WindowHeight = 50;

            ControlBank departament = new ControlBank();

            Bank sber = new Bank("Сбербанк", 7301, "Ульяновск", "Иванов");
            departament.RegBank(sber);

            Bank alfa = new Bank("Альфа-банк", 7302, "Ульяновск", "Петров");
            departament.RegBank(alfa);

            Bank vtb = new Bank("ВТБ", 7303, "Ульяновск", "Сидоров");
            departament.RegBank(vtb);

            Console.ReadKey();
            Console.Clear();

            Menu menu = new Menu(departament); 


        }
    }
}
