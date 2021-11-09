using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace p02_ClassesOOP
{
    public class MoneyBox
    {
        public static void Listen()
        {
            var bs = new BillingServer(new Repository());
            bs.Notify += (m) => { Console.WriteLine(m); };
            Console.WriteLine("press enter to continue");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Press 1 to add subscription");
                Console.WriteLine("Press 2 to break money box");
                var key = Console.ReadLine();
                ICard card = new Card();
                Console.WriteLine("enter card number");
                card.CardNumber = Console.ReadLine();
                Console.WriteLine("enter cvv");
                card.Cvv = int.Parse(Console.ReadLine());
                Console.WriteLine("enter summ");
                card.MonthlySum = int.Parse(Console.ReadLine());
                if (key == "1") 
                {
                    bs.Payment(card);
                }
                else if (key == "2")
                {
                    var refund = bs.Refund(card.CardNumber);
                }
                Console.WriteLine("press enter to continue");
            }
        }
    }

    public interface IRepository
    {
        int MoneyInfo(string cardNumber);
        int ReturnMoney(string cardNumber);
        void SummToMoneyBox(ICard card);
    }

    public class Repository : IRepository
    {
        private Dictionary<string, int> _db = new Dictionary<string, int>();
        public int MoneyInfo(string cardNumber)
        {
            return _db[cardNumber];
        }

        public int ReturnMoney(string cardNumber)
        {
            var money = _db[cardNumber];
            _db[cardNumber] = 0;
            return money;
        }

        public void SummToMoneyBox(ICard card)
        {
            if (_db.ContainsKey(card.CardNumber))
            {
                _db[card.CardNumber] += card.MonthlySum;
                return;
            }

            _db.Add(card.CardNumber, card.MonthlySum);
        }
    }

    public interface ICard
    {
        string CardNumber { get; set; }
        int Cvv { get; set; }
        int TotalSumm { get; set; }
        int MonthlySum { get; set; }
    }

    public class Card : ICard
    {
        public string CardNumber { get; set; }
        public int Cvv { get; set; }
        public int TotalSumm { get; set; }
        public int MonthlySum { get; set; }
    }

    public class BillingServer : IBillingServer
    {
        private readonly IRepository _repo;
        public delegate void PaymentHandler(string message);

        private List<ICard> _cardsToMonthlyPayments;

        public event PaymentHandler Notify;

        public BillingServer(IRepository repo)
        {
            _repo = repo;
            _cardsToMonthlyPayments = new List<ICard>();
            Task monthlyPayment = Task.Run(() =>
            {
                while (true)
                {
                    _cardsToMonthlyPayments.ForEach((c) =>
                    {
                        _repo.SummToMoneyBox(c);
                        Notify?.Invoke($"monthly payment {c.MonthlySum} was processed");
                        Notify?.Invoke($"Total sum is {_repo.MoneyInfo(c.CardNumber)}");
                    });

                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            });
        }
        public bool Payment(ICard card)
        {
            _repo.SummToMoneyBox(card);
            AddCardToMonthlyPayments(card);
            Notify?.Invoke($"first payment {card.MonthlySum} was processed");
            return true;
        }

        private void AddCardToMonthlyPayments(ICard card)
        {
            _cardsToMonthlyPayments.Add(card);
        }

        public int Refund(string cardNumber)
        {
            var money = _repo.ReturnMoney(cardNumber);
            _cardsToMonthlyPayments.Remove(_cardsToMonthlyPayments.Find((c) => c.CardNumber == cardNumber));
            return money;
        }
    }

    public class CurrentUserProvider
    {
        public static string GetToken()
        {
            return "someToken";
        }
    }

    public interface IBillingServer
    {
        bool Payment(ICard card);
        int Refund(string cardNumber);
    }
}