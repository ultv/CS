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
    [DataContract]
    public class Bank
    {
        [DataMember]
        public string Name { get; set; }        
        [DataMember]
        public const int Commission = 3;
        [DataMember]
        public int Profit { get; set; }
        [DataMember]
        public List<Client> Clients { get; set; }
        
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
            Console.WriteLine($"\n------------------------------------------------ ");
            Console.WriteLine($"\n{Name} зарегистрировал нового клиента - {client.Name}.");
        }

        public void MessageOpenBank()
        {
            Console.WriteLine($"\n------------------------------------------------ ");
            Console.WriteLine($"\n{Name} открыл новое отделение.");
            Console.WriteLine($"\nТекущий доход {Name}а составляет {Profit} рублей.");
        }

        public void OpenAccount(string name, int id, int money)
        {
            Account account = new Account(id, money);

            foreach (Client client in Clients)
            {
                if (name == client.Name)
                {
                    client.Accounts.Add(account);
                }
            }
            Console.WriteLine($"\n-------------- {account.whenOpen.ToString()} -------------- ");
            Console.WriteLine($"\n{name} открыл новый счёт - №{id} в {Name}. Баланс счёта {account.Balance} рублей.");
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
            Console.WriteLine($"\n------------------------------------------------ ");
            Console.WriteLine($"\n{name} пополнил счёт - №{id} в {Name} на {money} рублей. Баланс - {balance} рублей.");
        }

        public void InternalTransfer(int from, int to, int money)
        {
            string name = "";
            int balansFrom = 0;
            int balansTo = 0;

            foreach (Client client in Clients)
            {
                foreach (Account account in client.Accounts)
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
            Console.WriteLine($"\n------------------------------------------------ ");
            Console.WriteLine($"\n{name} перевел в {Name} - {money} рублей со счёта №{from} на счёт №{to}.");
            Console.WriteLine($"\nБаланс счёта №{from} составляет {balansFrom} рублей.");
            Console.WriteLine($"\nБаланс счёта №{to} составляет {balansTo} рублей.");
        }

        public void ExternalTransfer(int from, int to, Bank bank, int money)
        {
            string nameFrom = "";
            string nameTo = "";
            int balansFrom = 0;
            int balansTo = 0;
            int commission = 0;

            foreach (Client client in Clients)
            {
                foreach (Account account in client.Accounts)
                {
                    if (from == account.ID)
                    {
                        commission = (money / 100) * Commission;
                        Profit = Profit + commission;

                        account.Balance = account.Balance - money - commission;
                        balansFrom = account.Balance;
                        nameFrom = client.Name;
                    }
                }
            }

            foreach (Client client in bank.Clients)
            {
                foreach (Account account in client.Accounts)
                {
                    if (to == account.ID)
                    {
                        account.Balance = account.Balance + money;
                        balansTo = account.Balance;
                        nameTo = client.Name;
                    }
                }
            }
            Console.WriteLine($"\n------------------------------------------------ ");
            Console.WriteLine($"\n{nameFrom} перевел из {Name} - {money} рублей\n\nсо счёта №{from} на счёт №{to} в {bank.Name} {nameTo}");
            Console.WriteLine($"\nКомиссия за перевод составила {commission} рублей.\n\nТекущий доход {Name}а  составляет {Profit} рублей.");
            Console.WriteLine($"\nБаланс счёта №{from} составляет {balansFrom} рублей.");
            Console.WriteLine($"\nБаланс счёта №{to} составляет {balansTo} рублей.");

        }
    }

    public class Client
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }

        public Client() { }

        public Client(string name)
        {
            Accounts = new List<Account>();
            Name = name;
        }
    }

    public class Account
    {
        public int ID { get; set; }
        public int Balance { get; set; }
        public DateTime whenOpen;

        public Account() { }

        public Account(int id, int money)
        {
            ID = id;
            Balance = money;
            whenOpen = DateTime.Now;
        }
    }


    class Program
    {               

        static void Main(string[] args)
        {

            Bank sber = new Bank("Сбербанк");

            sber.RegClient("Александр");
            sber.OpenAccount("Александр", 1001, 100);
            sber.PutMoney(1001, 200);

            sber.OpenAccount("Александр", 1002, 200);
            sber.InternalTransfer(1001, 1002, 150);

            Bank alfa = new Bank("Альфа-банк");

            alfa.RegClient("Александр");
            alfa.OpenAccount("Александр", 2001, 50);
            alfa.PutMoney(2001, 500);

            sber.ExternalTransfer(1002, 2001, alfa, 100);
            alfa.ExternalTransfer(2001, 1001, sber, 200);
            alfa.ExternalTransfer(2001, 1001, sber, 100);
            alfa.ExternalTransfer(2001, 1001, sber, 100);

            alfa.RegClient("Дмитрий");
            alfa.OpenAccount("Дмитрий", 2002, 500);

            sber.ExternalTransfer(1001, 2002, alfa, 100);




            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Bank));

            using (FileStream fs = new FileStream("Bank.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, alfa);
                Console.WriteLine("\n=========================");
                Console.WriteLine("\nОбъект Bank сериализован.");
            }

            using (FileStream fs = new FileStream("Bank.json", FileMode.OpenOrCreate))
            {
                Bank desAlfa = (Bank)jsonFormatter.ReadObject(fs);
                Console.WriteLine("\nОбъект Bank десериализован.");
                Console.WriteLine($"\nНазвание: {desAlfa.Name} - Текущий доход: {desAlfa.Profit}");
            }


            Console.ReadKey();

        }
    }
}
