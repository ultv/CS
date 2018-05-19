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
        }





        static void Main(string[] args)
        {

            ControlBank departament = new ControlBank();

            Bank sber = new Bank("Сбербанк", 7301, "Ульяновск", "Иванов");
            departament.RegBank(sber);

            Bank alfa = new Bank("Альфа-банк", 7302, "Ульяновск", "Петров");
            departament.RegBank(alfa);

            Bank vtb = new Bank("ВТБ", 7303, "Ульяновск", "Сидоров");
            departament.RegBank(vtb);




            Console.ReadKey();

        }
    }
}
