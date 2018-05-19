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
            public Account() { }
        }

        // Данные клиента.
        [DataContract]
        class Client
        {
            [DataMember]
            public string firstName { get; set; }
            [DataMember]
            public string surName { get; set; }
            //[DataMember]
            public DateTime DateReg { get; set; }
            //[DataMember]
            public Account Accouts = new Account();

            public Client(string fName, string sName)
            {
                firstName = fName;
                surName = sName;
                DateReg = DateTime.Now;
            }
                

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
            //[DataMember]
            public DateTime DateOpen { get; set; }

            public Bank(string name, int id, string sity, string director)
            {
                Name = name;
                IDBank = id;
                Sity = sity;
                Director = director;
                DateOpen = DateTime.Now;                

                Console.WriteLine($"\n{delimiter} {DateOpen.ToLongDateString()} {delimiter}");
                Console.WriteLine($"\nОткрыто отделение {Name}а №{IDBank} в городе {Sity}.");

                //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Bank));

                //using (FileStream fs = new FileStream("BankSystem_Banks.json", FileMode.Append))
                //{
                //    jsonFormatter.WriteObject(fs, this);                    
                //}

            }

            public void RegClient(string fName, string sName)
            {
                Client client = new Client(fName, sName);
                Clients.Add(client);
            }

            public void ShowClients()
            {
                Console.WriteLine($"\nКлиенты {Name}а:");

                foreach (Client client in Clients)
                {
                    Console.WriteLine($"\n{client.firstName} {client.surName}.");
                }
            }
        }

        // Данные о всех банках.
        [DataContract]
        class ControlBank
        {
            [DataMember]
            public List<Bank> Banks = new List<Bank>();

            public ControlBank()
            {
                //Console.WriteLine("\nСоздана система управления банками.");                
            }

            // Регистрирует новый банк.
            public void RegBank(string name, int id, string sity, string director)
            {
                Bank bank = new Bank(name, id, sity, director);
                Banks.Add(bank);
                Console.WriteLine($"\n{delimiter} {bank.DateOpen.ToLongDateString()} {delimiter}");
                Console.WriteLine($"\n{bank.Name} зарегистрирован в системе управлениеями банками.");
            }

            public ControlBank LoadControlBank()
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ControlBank));
                ControlBank dep = new ControlBank();

                using (FileStream fs = new FileStream("BankSystem_ControlBank.json", FileMode.Open))
                {
                    dep = (ControlBank)jsonFormatter.ReadObject(fs);                    
                }

                return dep;
            }

            public void SaveBanks() ///--del
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Bank));

                using (FileStream fs = new FileStream("BankSystem_Banks.json", FileMode.Append))
                {
                    foreach(Bank bank in Banks)
                    {
                        jsonFormatter.WriteObject(fs, bank);
                    }                    
                }
            }

            public void SaveControlBank()
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ControlBank));

                using (FileStream fs = new FileStream("BankSystem_ControlBank.json", FileMode.OpenOrCreate))
                {                    
                    jsonFormatter.WriteObject(fs, this);                    
                }
            }

            public void ShowBanks()
            {
                Console.Clear();
          //      Console.SetCursorPosition(0, positionContent);
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
                    //         Console.SetCursorPosition(0, 0);                    
                    Console.WriteLine($"\n{delimiter}{delimiter}");
                    Console.WriteLine("\nДоступные действия:");
                    Console.WriteLine($"\n{delimiter}{delimiter}");
                    Console.WriteLine("\n1 - Показать банки.");
                    Console.WriteLine("\n2 - Открыть новое отделение банка.");
                    Console.WriteLine("\n3 - Зарегистрировать клиента.");
                    Console.WriteLine("\n4 - Показать клиентов банка.");
                    Console.WriteLine("\n0 - Выход.");
                    Console.WriteLine($"\n{delimiter}{delimiter}\n");

                    while (!correctIn)
                    {
                        try
                        {
                            choice = Int32.Parse(Console.ReadLine());

                            if ((choice < 0) || (choice > 4))
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
                            Console.WriteLine("\nВведите название банка:\n");
                            string name = Console.ReadLine();
                            bool ok = false;
                            int id = 0;

                            while (!ok)
                            {
                                Console.WriteLine("\nВведите номер отделения:\n");
                                try
                                {
                                    id = Int32.Parse(Console.ReadLine());
                                    ok = true;
                                }
                                catch
                                {
                                    Console.WriteLine("\nНомер банка - числовое значение. Повторите ввод:\n");
                                    ok = false;
                                }
                            }                                                        

                            Console.WriteLine("\nВведите название города:\n");
                            string sity = Console.ReadLine();

                            Console.WriteLine("\nВведите фамилию управляющего:\n");
                            string director = Console.ReadLine();

                            dep.RegBank(name, id, sity, director);
                            break;

                        case 3:

                            string fName = "";
                            string sName = "";
                            string bName = "";

                            Console.WriteLine("\nВведите название банка:\n");
                            bName = Console.ReadLine();

                            Console.WriteLine("\nВведите имя клиента:\n");
                            fName = Console.ReadLine();

                            Console.WriteLine("\nВведите фамилию клиента:\n");
                            sName = Console.ReadLine();

                            Client client = new Client(fName, sName);

                            foreach(Bank bank in dep.Banks)
                            {
                                if (bank.Name == bName)
                                {
                                    bank.Clients.Add(client);
                                }
                            }

                            break;

                        case 4:

                            string bbName = "";
                            Console.WriteLine("\nВведите название банка:\n");
                            bbName = Console.ReadLine();

                            foreach (Bank bank in dep.Banks)
                            {
                                if (bank.Name == bbName)
                                {
                                    bank.ShowClients();
                                }
                            }

                            break;

                        case 0:

                            dep.SaveControlBank();
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
            departament = departament.LoadControlBank();
            

            //Console.ReadKey();
            //Console.Clear();

            Menu menu = new Menu(departament); 


        }
    }
}
