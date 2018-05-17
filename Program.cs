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
        
        class Bank
        {
            public string Name { get; set; }
            public List<Client> Clients { get; set; }
            public int Commission { get; set; }

            public Bank( ) { }

            public Bank(string name)
            {
                Name = name;
                Clients = new List<Client>();
                MessageOpenBank();
            }

            public void RegClient(string name)
            {
                Client client = new Client(name);
                Clients.Add(client);
                Console.WriteLine($"\n{Name} зарегистрировал нового клиента - {client.Name}.");
            }

            public void MessageOpenBank( )
            {
                Console.WriteLine($"\n{Name} открыл новое отделение.");
            }

            public void OpenAccount(string name, int id, int money)
            {
                Account account = new Account(id, money);

                foreach(Client client in Clients)
                {
                    if(name == client.Name)
                    {
                        client.Accounts.Add(account);                        
                    }
                }                
                Console.WriteLine($"\n{name} открыл новый счёт - №{id} в {Name}.");
            }

            public void PutMoney(int id, int money)
            {
                string name = "";
                int balance = 0;

                foreach (Client client in Clients)
                {
                    foreach (Account account in client.Accounts)
                    {
                        if (id == account.ID)
                        {
                            account.Balance = account.Balance + money;
                            name = client.Name;
                            balance = account.Balance;
                        }
                    }                    
                }
                Console.WriteLine($"\n{name} пополнил счёт - №{id} в {Name} на {money} рублей. Баланс - {balance} рублей.");
            }

            public void InternalTransfer(int from, int to, int money)
            {
                string name = "";
                int balansFrom = 0;
                int balansTo = 0;

                foreach (Client client in Clients)
                {
                    foreach(Account account in client.Accounts)
                    {
                        if (from == account.ID)
                        {
                            account.Balance = account.Balance - money;
                            balansFrom = account.Balance;
                            name = client.Name;
                        }
                        if (to == account.ID)
                        {
                            account.Balance = account.Balance + money;
                            balansTo = account.Balance;
                        }
                    }

                }

                Console.WriteLine($"\n{name} перевел в {Name} - {money} рублей со счёта №{from} на счёт №{to}.");
                Console.WriteLine($"\nБаланс счёта №{from} составляет {balansFrom} рублей.");
                Console.WriteLine($"\nБаланс счёта №{to} составляет {balansTo} рублей.");                
            }

        }

        class Client
        {
            public string Name { get; set; }
            public List<Account> Accounts { get; set; }

            public Client( ) { }

            public Client(string name)
            {
                Accounts = new List<Account>();
                Name = name;
            }           
        }

        class Account
        {
            public int ID { get; set; }
            public int Balance { get; set; }

            public Account( ) { }

            public Account(int id, int money)
            {
                ID = id;
                Balance = money;
                Console.WriteLine($"\nНа созданном счету №{id} находится {Balance} рублей.");
            }
        }




        static void Main(string[] args)
        {

            Bank sber = new Bank("Сбербанк");

            sber.RegClient("Александр");
            sber.OpenAccount("Александр", 1001, 100);
            sber.PutMoney(1001, 200);

            Bank alfa = new Bank("Альфа-банк");
            alfa.RegClient("Александр");
            alfa.OpenAccount("Александр", 2001, 50);
            alfa.PutMoney(2001, 500);

            sber.OpenAccount("Александр", 1002, 200);
            sber.InternalTransfer(1001, 1002, 150);







            Console.ReadKey();

        }
    }
}
