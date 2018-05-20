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
    //    enum VALUTA { RUB, USD, EUR};

        // Счёт клиента.
        [DataContract]
        class Account
        {
            [DataMember]
            public int IDAccount { get; set; }
            [DataMember]
            public int Balance { get; set; }
            [DataMember]
            public string Valuta { get; set; }

            public Account() { }

            public Account(int id, int money, string valuta)
            {
                IDAccount = id;
                Balance = Balance + money;
                Valuta = valuta;
            }
        }

        // Данные клиента.
        [DataContract]
        class Client
        {
            [DataMember]
            public string firstName { get; set; }
            [DataMember]
            public string surName { get; set; }
            [DataMember]
            public DateTime DateReg { get; set; }
            [DataMember]
            public List<Account> Accounts = new List<Account>();

            public Client() { }

            public Client(string fName, string sName)
            {
                firstName = fName;
                surName = sName;                
                DateReg = DateTime.Now;                
            }

            public void OpenAccount(int id, int money, string valuta)
            {
                Account acnt = new Account(id, money, valuta);
                Accounts.Add(acnt);                
                Console.WriteLine($"\nБаланс счёта {acnt.Balance} {valuta}.");
            }
                

        }

        // Операция со счетами.
        [DataContract]
        class Transaction
        {
            [DataMember]
            public DateTime DateTransaction;
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public int FromAccount { get; set; }
            [DataMember]
            public int ToAccount { get; set; }
            [DataMember]
            public int Money { get; set; }
            [DataMember]
            public string Valuta { get; set; }            

            public Transaction(int from, int to, int money, string valuta)
            {
                DateTransaction = DateTime.Now;
                FromAccount = from;
                ToAccount = to;
                Money = money;
                Valuta = valuta;
            }
            
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
            public List<Transaction> LogTrans = new List<Transaction>();
            [DataMember]
            public DateTime DateOpen { get; set; }

            public Bank() { }

            public Bank(string name, int id, string sity, string director)
            {
                Name = name;
                IDBank = id;
                Sity = sity;
                Director = director;
                DateOpen = DateTime.Now;                

                Console.WriteLine($"\n{delimiter} {DateOpen.ToLongDateString()} {delimiter}");
                Console.WriteLine($"\nОткрыто отделение {Name}а №{IDBank} в городе {Sity}.");
                
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

            // JSON
            public ControlBank LoadControlBank()
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ControlBank));
                ControlBank dep = new ControlBank();

                using (FileStream fs = new FileStream("BankSystem.json", FileMode.Open))
                {
                    try
                    {
                        dep = (ControlBank)jsonFormatter.ReadObject(fs);
                    }
                    catch
                    {

                    }                    
                }

                return dep;
            }           

            // JSON
            public void SaveControlBank()
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(ControlBank));

                using (FileStream fs = new FileStream("BankSystem.json", FileMode.OpenOrCreate))
                {                    
                    jsonFormatter.WriteObject(fs, this);                    
                }
            }

            public void ShowBanks()
            {
                Console.Clear();          
                Console.WriteLine($"\nВ системе управления банками зарегистрированы:");

                foreach (Bank bank in Banks)
                {                    
                    Console.WriteLine($"\n{bank.Name}.");
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
                    
                    Console.WriteLine($"\n{delimiter}{delimiter}");
                    Console.WriteLine("\nДоступные действия:");
                    Console.WriteLine($"\n{delimiter}{delimiter}");
                    Console.WriteLine("\n1 - Показать банки / клиентов банка.");                    
                    Console.WriteLine("\n2 - Показать счета клиента.");
                    Console.WriteLine("\n3 - Показать: банки - клиенты - счета.");
                    Console.WriteLine("\n4 - Показать историю транзакций.");
                    Console.WriteLine($"\n{delimiter}{delimiter}");
                    Console.WriteLine("\n5 - Открыть новое отделение банка.");
                    Console.WriteLine("\n6 - Зарегистрировать клиента.");
                    Console.WriteLine("\n7 - Открыть счёт.");
                    Console.WriteLine("\n8 - Внесение средств.");
                    Console.WriteLine("\n9 - Перевод средств.");
                    Console.WriteLine("\n0 - Выход.");
                    Console.WriteLine($"\n{delimiter}{delimiter}\n");

                    while (!correctIn)
                    {
                        try
                        {
                            choice = Int32.Parse(Console.ReadLine());

                            if ((choice < 0) || (choice > 9))
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
                    
                    switch (choice )
                    {                        

                        case 1:
                     
                            ItemShowBanksClients(dep);                            
                            break;

                        case 2:

                            ItemShowAccount(dep);
                            break;

                        case 3:

                            ItemShowDetail(dep);
                            break;

                        case 4:

                            ItemShowLogTrans(dep);
                            break;

                        case 5:

                            ItemOpenBank(dep);
                            break;

                        case 6:

                            ItemRegClient(dep);
                            break;

                        case 7:

                            ItemOpenAccount(dep);
                            break;

                        case 8:

                            ItemDeposit(dep);
                            break;

                        case 9:

                            ItemTransfer(dep);
                            break;

                        case 0:

                            dep.SaveControlBank();
                            exit = true;
                            break;
                    }
                }
            }

            public void ItemShowBanksClients(ControlBank dep)
            {
                dep.ShowBanks();
                ItemShowClients(dep);

            }

            // Пункт меню - Открытие нового отделения банка.
            public void ItemOpenBank(ControlBank dep)
            {

                int idBank = 0;
                bool ok = false;

                Console.WriteLine("\nВведите название банка:\n");
                string bankName = Console.ReadLine();                

                while (!ok)
                {
                    Console.WriteLine("\nВведите номер отделения:\n");
                    try
                    {
                        idBank = Int32.Parse(Console.ReadLine());
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
                Console.Clear();

                dep.RegBank(bankName, idBank, sity, director);
            }
            
            // Пункт меню - Регистрация клиента.
            public void ItemRegClient(ControlBank dep)
            {
                Console.WriteLine("\nВведите название банка:\n");
                string bankName = Console.ReadLine();

                Console.WriteLine("\nВведите имя клиента:\n");
                string firstName = Console.ReadLine();

                Console.WriteLine("\nВведите фамилию клиента:\n");
                string surName = Console.ReadLine();
                Console.Clear();

                Client client = new Client(firstName, surName);

                foreach (Bank bank in dep.Banks)
                {
                    if (bank.Name == bankName)
                    {
                        bank.Clients.Add(client);
                        Console.WriteLine($"\n{firstName} {surName} зарегистрирован в {bankName}е.");
                    }
                }
            }
            
            
            // Пункт меню - Показать клиентов.
            public void ItemShowClients(ControlBank dep)
            {
                Console.WriteLine($"\n{delimiter}{delimiter}");
                Console.WriteLine("\nВведите название банка для просмотра его клиентов: (0 - вернуться в меню)\n");
                string bankName = Console.ReadLine();
                if(bankName != "0")
                {
                    Console.Clear();

                    foreach (Bank bank in dep.Banks)
                    {
                        if (bank.Name == bankName)
                        {
                            bank.ShowClients();
                        }
                    }
                }
                
            }

            // Пункт меню - Открыть счёт.
            public void ItemOpenAccount(ControlBank dep)
            {

                Console.WriteLine("\nВведите название банка:\n");
                string bankName = Console.ReadLine();

                Console.WriteLine("\nВведите имя клиента:\n");
                string firstName = Console.ReadLine();

                Console.WriteLine("\nВведите фамилию клиента:\n");
                string surName = Console.ReadLine();

                Console.WriteLine("\nВведите номер счёта:\n");
                int idAccount = Int32.Parse(Console.ReadLine());

                Console.WriteLine("\nВведите сумму:\n");
                int money = Int32.Parse(Console.ReadLine());

                Console.WriteLine("\nВведите валюту счёта (RUB, USD, EUR):\n");
                string valuta = Console.ReadLine();
                Console.Clear();

                foreach (Bank bank in dep.Banks)
                {
                    if (bank.Name == bankName)
                    {
                        foreach (Client client in bank.Clients)
                        {
                            if ((client.firstName == firstName) && (client.surName == surName))
                            {
                                client.OpenAccount(idAccount, money, valuta);
                                Console.WriteLine($"\n{firstName} {surName} открыл счёт №{idAccount} в {bankName}е.");
                                Transaction deposit = new Transaction(0, idAccount, money, valuta);
                                deposit.Name = "Открытие";
                                bank.LogTrans.Add(deposit);
                            }

                        }
                    }
                }
            }

            // Пункт меню - Показать счета клиента.
            public void ItemShowAccount(ControlBank dep)
            {
                
                Console.WriteLine("\nВведите имя клиента:\n");
                string firstName = Console.ReadLine();

                Console.WriteLine("\nВведите фамилию клиента:\n");
                string surName = Console.ReadLine();
                Console.Clear();
                
                foreach (Bank bank in dep.Banks)
                {                    
                    foreach (Client client in bank.Clients)
                    {
                        if ((client.firstName == firstName) && (client.surName == surName))
                        {
                            Console.WriteLine($"\n{client.firstName} {client.surName} владеет счетами в {bank.Name}е:");
                            foreach (Account account in client.Accounts)
                            {
                                Console.WriteLine($"\n\tСчёт №{account.IDAccount}. Баланс {account.Balance} {account.Valuta}.");
                            }                            
                        }
                    }                    
                }
            }

            // Пункт меню - показать информацию по банкам, клиентам и счетам.
            public void ItemShowDetail(ControlBank dep)
            {
                Console.Clear();

                foreach (Bank bank in dep.Banks)
                {
                    Console.WriteLine($"\n{bank.Name}:");
                    foreach(Client client in bank.Clients)
                    {
                        Console.WriteLine($"\n\t{client.firstName} {client.surName}:");
                        foreach(Account account in client.Accounts)
                        {
                            Console.WriteLine($"\n\tсчёт №{account.IDAccount}. Баланс {account.Balance} {account.Valuta}.");
                        }

                    }
                }
            }

            // Пункт - меню осуществить перевод
            public void ItemTransfer(ControlBank dep)
            {
                Console.WriteLine("\nНомер счёта с которого осуществляется перевод:\n");
                int from = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\nНомер счёта на который осуществляется перевод:\n");
                int to = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\nСумма перевода:\n");
                int money = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\nВалюта (RUB, USD, EUR):\n");
                string valuta = Console.ReadLine();
                Console.Clear();

                Transaction transFrom = new Transaction(from, to, money, valuta);
                Transaction transTo = new Transaction(from, to, money, valuta);

                foreach (Bank bank in dep.Banks)
                {                    

                    foreach (Client client in bank.Clients)
                    {
                        foreach(Account account in client.Accounts)
                        {
                            if(account.IDAccount == from)
                            {
                                account.Balance = account.Balance - money;
                                Console.WriteLine($"\nСо счёта (№{from} - {bank.Name}) переведено {money} {valuta}. Баланс {account.Balance} {account.Valuta}.");
                                transFrom.Name = "Списание";
                                bank.LogTrans.Add(transFrom);
                            }
                            if (account.IDAccount == to)
                            {
                                account.Balance = account.Balance + money;
                                Console.WriteLine($"\nНа счёт (№{to} - {bank.Name}) поступило {money} {valuta}. Баланс {account.Balance} {account.Valuta}.");
                                transTo.Name = "Зачисление";
                                bank.LogTrans.Add(transTo);                                                                
                            }
                        }
                    }
                }
            }

            public void ItemDeposit(ControlBank dep)
            {                
                Console.WriteLine("\nНомер счёта для внесения средств:\n");
                int deposit = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\nСумма:\n");
                int money = Int32.Parse(Console.ReadLine());
                Console.WriteLine("\nВалюта (RUB, USD, EUR):\n");
                string valuta = Console.ReadLine();
                Console.Clear();

                foreach (Bank bank in dep.Banks)
                {

                    foreach (Client client in bank.Clients)
                    {
                        foreach (Account account in client.Accounts)
                        {                            
                            if (account.IDAccount == deposit)
                            {
                                Transaction transDeposit = new Transaction(0, deposit, money, valuta);
                                account.Balance = account.Balance + money;
                                Console.WriteLine($"\nНа счёт (№{deposit} - {bank.Name}) поступило {money} {valuta}. Баланс {account.Balance} {account.Valuta}.");
                                transDeposit.Name = "Внесение";
                                bank.LogTrans.Add(transDeposit);
                            }
                        }
                    }
                }

            }

            // Показать историю операций.
            public void ItemShowLogTrans(ControlBank dep)
            {
                Console.Clear();

                foreach (Bank bank in dep.Banks)
                {
                    Console.WriteLine($"\n{bank.Name}. Отделение №{bank.IDBank}:");

                    foreach(Transaction trans in bank.LogTrans)
                    {                     

                        if (trans.Name == "Списание")
                        {
                            string toBank = FindBankByAccount(dep, trans.ToAccount);
                            Console.WriteLine($"\n\t{trans.DateTransaction.ToLongDateString()} {trans.DateTransaction.ToShortTimeString()}");
                            Console.WriteLine($"\n\tСо счёта №{trans.FromAccount} переведено {trans.Money} {trans.Valuta} на счёт (№{trans.ToAccount} - {toBank}).");
                        }
                        else if (trans.Name == "Зачисление")
                        {
                            string fromBank = FindBankByAccount(dep, trans.FromAccount);
                            Console.WriteLine($"\n\t{trans.DateTransaction.ToLongDateString()} {trans.DateTransaction.ToShortTimeString()}");
                            Console.WriteLine($"\n\tНа счёт №{trans.ToAccount} поступило {trans.Money} {trans.Valuta} со счёта (№{trans.FromAccount} - {fromBank}).");
                        }
                        else if (trans.Name == "Внесение")
                        {
                            Console.WriteLine($"\n\t{trans.DateTransaction.ToLongDateString()} {trans.DateTransaction.ToShortTimeString()}");
                            Console.WriteLine($"\n\tНа счёт №{trans.ToAccount} внесено {trans.Money} {trans.Valuta}.");
                        }
                        else if (trans.Name == "Открытие")
                        {
                            Console.WriteLine($"\n\t{trans.DateTransaction.ToLongDateString()} {trans.DateTransaction.ToShortTimeString()}");
                            Console.WriteLine($"\n\tНа счёт №{trans.ToAccount} при открытии внесено {trans.Money} {trans.Valuta}.");
                        }
                    }
                }
            }

            // Найти банк по номеру счёта.
            public string FindBankByAccount(ControlBank dep, int account)
            {
                foreach(Bank bank in dep.Banks)
                {
                    foreach(Client client in bank.Clients)
                    {
                        foreach(Account acnt in client.Accounts)
                        {
                            if (account == acnt.IDAccount)
                                return bank.Name;
                        }
                    }
                }

                return "";
            }

        }



        static void Main(string[] args)
        {
            Console.Clear();
            Console.WindowHeight = 72;

            ControlBank departament = new ControlBank();

            departament = departament.LoadControlBank();            

            Menu menu = new Menu(departament); 

        }
    }
}
