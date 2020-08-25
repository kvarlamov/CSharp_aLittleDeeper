using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Security;

namespace p04_Linq
{
    public static class AtmSimulator
    {
        public static List<User> Users { get; private set; }
        public static List<Account> Accounts { get; private set; }
        public static List<History> History { get; private set; }
        
        static AtmSimulator()
        {
            Users = Data.GetUsers();
            Accounts = Data.GetAccounts();
            History = Data.GetHistory();
        }

        public static void Start()
        {
            Console.WriteLine("WELCOME TO ATM SIMULATOR");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter option:\n" +
                                  "[1] - show account info by login & password\n" +
                                  "[2] - show all account of user\n" +
                                  "[3] - show all account of user, include history for each accpunt\n" +
                                  "[4] - show refill operations and account owner\n" +
                                  "[5] - show users with account sum more than:"
                                  );
                int key = 0;
                int.TryParse(Console.ReadLine(), out key);
                if (key == 0)
                    continue;
                switch (key)
                {
                    case 1:
                        Console.WriteLine("enter login");
                        var login = Console.ReadLine();
                        Console.WriteLine("enter passrord");
                        var password = Console.ReadLine();
                        var user = Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                        if (user == null)
                        {
                            Console.WriteLine("User with those login and password not found ");
                            break;
                        }

                        var account = Accounts.FirstOrDefault(a => a.UserId == user.Id);
                        if (account != null)
                        {
                            Console.WriteLine($"Account open date: {account.CreateDate}, Account ammount: {account.Sum}, Owner: {user.FIO}");
                        }
                        break;
                    case 2:
                        Console.WriteLine("enter login");
                        var userLogin = Console.ReadLine();
                        var userr = Users.FirstOrDefault(u => u.Login == userLogin);
                        if (userr != null)
                        {
                            Accounts.Where(a => a.UserId == userr.Id).ToList().ForEach(x =>
                            {
                                Console.WriteLine($"Account open date: {x.CreateDate}, Account ammount: {x.Sum}, Owner: {userr.FIO}");
                            });
                        }
                        else
                        {
                            Console.WriteLine("user not found");
                        }
                        break;
                    case 3:
                        Console.WriteLine("enter login");
                        var userLogin3 = Console.ReadLine();
                        var user3 = Users.FirstOrDefault(u => u.Login == userLogin3);
                        if (user3 != null)
                        {
                            var accounts = from a in Accounts
                                            where a.UserId == user3.Id
                                            from h in History
                                            where h.AccountId == a.Id
                                            select new
                                            {
                                                OpenDate = a.CreateDate,
                                                Amount = a.Sum,
                                                Owner = user3.FIO,
                                                Operation = Enum.GetName(typeof(OperationType), h.OperationType)
                                            };
                            accounts.ToList().ForEach(a =>
                            {
                                Console.WriteLine($"OpenDate: {a.OpenDate}, Amount: {a.Amount}, Owner: {a.Owner}, Operation: {a.Operation}");
                            });
                        }
                        else
                        {
                            Console.WriteLine("user not found");
                        }
                        break;
                    case 4:
                        var withdrawals = from h in History
                                            where h.OperationType == OperationType.Refill
                                            from a in Accounts
                                            where a.Id == h.AccountId
                                            from u in Users
                                            where u.Id == a.UserId
                                            select new
                                            {
                                                AccountOwner = u.FIO,
                                                AccountId = a.Id,
                                                OperationDate = h.OperationDate,
                                                operationSum = h.Sum,
                                                opId = h.Id
                                            };
                        withdrawals.ToList().ForEach(x =>
                        {
                            Console.WriteLine($"Account owner: {x.AccountOwner}, operation sum: {x.operationSum}, opId: {x.opId}, AccountId: {x.AccountId}");
                        });
                        break;
                    case 5:
                        Console.WriteLine("Show account with sum more than:");
                        var sum = long.TryParse(Console.ReadLine(), out long result);
                        if (!sum)
                        {
                            Console.WriteLine("incorrect input");
                            break;
                        }

                        var users = from a in Accounts
                                        where a.Sum > result
                                        from u in Users
                                        where u.Id == a.Id
                                        select new
                                        {
                                            FIO = u.FIO,
                                            Amount = a.Sum,
                                            AccountId = a.Id
                                        };
                        users.ToList().ForEach(x =>
                        {
                            Console.WriteLine($"FIO: {x.FIO}, Amount: {x.Amount}, AccountId: {x.AccountId}");
                        });
                        break;
                }
            }
        }
    }

    public static class Data
    {
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1, Firstname = "Иван", Lastname = "Степанов", Patronymic = "Дмитриевич",
                    Login = "stepanovid", Password = "123", Phone = "9564422"
                },
                new User
                {
                    Id = 2, Firstname = "Петр", Lastname = "Иванов", Patronymic = "Иванович",
                    Login = "ivanovpi", Password = "123", Phone = "9564421"
                },
                new User
                {
                    Id = 3, Firstname = "Дмитрий", Lastname = "Цой", Patronymic = "Степанович",
                    Login = "coids", Password = "123", Phone = "9564425"
                },
                new User
                {
                    Id = 4, Firstname = "Иван", Lastname = "Булкин", Patronymic = "Петрович",
                    Login = "bulkinip", Password = "123", Phone = "9564429"
                },
                new User
                {
                    Id = 5, Firstname = "Богдан", Lastname = "Бобров", Patronymic = "Алексеевич",
                    Login = "bobrovba", Password = "123", Phone = "9564432"
                },
                new User
                {
                    Id = 5, Firstname = "Роман", Lastname = "Лисицын", Patronymic = "Романович",
                    Login = "lisicinrr", Password = "123", Phone = "9564462"
                },
                new User
                {
                    Id = 6, Firstname = "Сергей", Lastname = "Ким", Patronymic = "", Login = "kims",
                    Password = "123", Phone = "9564422"
                },
                new User
                {
                    Id = 7, Firstname = "Илья", Lastname = "Больших", Patronymic = "Дмитриевич",
                    Login = "bolshihid", Password = "123", Phone = "9564431"
                },
            };
        }
        
        public static List<Account> GetAccounts()
        {
            return new List<Account>
            {
                new Account {Id = 1, Sum = 100000, CreateDate = DateTime.Now, UserId = 2},
                new Account {Id = 2, Sum = 1000000, CreateDate = DateTime.Now, UserId = 1},
                new Account {Id = 3, Sum = 500000, CreateDate = DateTime.Now, UserId = 5},
                new Account {Id = 4, Sum = 3200000, CreateDate = DateTime.Now, UserId = 3},
                new Account {Id = 5, Sum = 7800000, CreateDate = DateTime.Now, UserId = 4},
                new Account {Id = 6, Sum = 10000000, CreateDate = DateTime.Now, UserId = 7},
                new Account {Id = 7, Sum = 10000, CreateDate = DateTime.Now, UserId = 6}
            };
        }
        
        public static List<History> GetHistory()
        {
            return new List<History>
            {
                new History{ Id = 1, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 2, Sum = 51000},
                new History{ Id = 2, OperationDate = DateTime.UtcNow, OperationType = OperationType.Credit, AccountId = 1, Sum = 15000},
                new History{ Id = 3, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 5, Sum = 500},
                new History{ Id = 4, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 1, Sum = 7000},
                new History{ Id = 5, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 2, Sum = 50500},
                new History{ Id = 6, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 2, Sum = 155000},
                new History{ Id = 7, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 3, Sum = 500000},
                new History{ Id = 8, OperationDate = DateTime.UtcNow, OperationType = OperationType.Credit, AccountId = 3, Sum = 5000000},
                new History{ Id = 9, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 6, Sum = 1005000},
                new History{ Id = 10, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 6, Sum = 55000},
                new History{ Id = 11, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 4, Sum = 50},
                new History{ Id = 12, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 2, Sum = 51000},
                new History{ Id = 13, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 3, Sum = 5000},
                new History{ Id = 14, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 4, Sum = 10050},
                new History{ Id = 15, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 4, Sum = 33000},
                new History{ Id = 16, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 2, Sum = 50200},
                new History{ Id = 17, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 2, Sum = 10000},
                new History{ Id = 18, OperationDate = DateTime.UtcNow, OperationType = OperationType.Credit, AccountId = 1, Sum = 12300},
                new History{ Id = 19, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 6, Sum = 1200},
                new History{ Id = 20, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 2, Sum = 1000},
                new History{ Id = 21, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 5, Sum = 85000},
                new History{ Id = 22, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 2, Sum = 15000},
                new History{ Id = 23, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 5, Sum = 95000},
                new History{ Id = 24, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 2, Sum = 750000},
                new History{ Id = 25, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 5, Sum = 15000},
                new History{ Id = 26, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 5, Sum = 600},
                new History{ Id = 28, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 7, Sum = 1000},
                new History{ Id = 29, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 7, Sum = 5000},
                new History{ Id = 30, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 2, Sum = 1000},
                new History{ Id = 31, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 7, Sum = 7000},
                new History{ Id = 32, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 5, Sum = 4000},
                new History{ Id = 33, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 7, Sum = 500000},
                new History{ Id = 34, OperationDate = DateTime.UtcNow, OperationType = OperationType.Refill, AccountId = 7, Sum = 100000},
                new History{ Id = 35, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 5, Sum = 4000},
                new History{ Id = 36, OperationDate = DateTime.UtcNow, OperationType = OperationType.Withdrawal, AccountId = 6, Sum = 3000}
            };
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public int DocSerie { get; set; }
        public int DocNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO => $"{Lastname} {Firstname} {Patronymic}";
    }

    public class Account
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Sum { get; set; }
        public int UserId { get; set; }
    }

    public class History
    {
        public int Id { get; set; }
        public DateTime OperationDate { get; set; }
        public OperationType OperationType { get; set; }
        public int AccountId { get; set; }
        public decimal Sum { get; set; }
    }

    public enum OperationType
    {
        Withdrawal = 1,
        Refill = 2 , 
        Credit = 3
    }
}