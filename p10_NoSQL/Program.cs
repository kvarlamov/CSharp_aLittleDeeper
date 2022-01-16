using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace p10_NoSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
            var db = client.GetDatabase("local");
            var user = new User()
            {
                Age = 33,
                Id = 1,
                FirstName = "Test",
                LastName = "User"
            };

            var users = new List<User>()
            {
                new User()
                {
                    Age = 29,
                    Id = 2,
                    FirstName = "Test2",
                    LastName = "User2"
                },
                new User()
                {
                    Age = 31,
                    Id = 3,
                    FirstName = "Test3",
                    LastName = "User3"
                },
                new User()
                {
                    Age = 40,
                    Id = 4,
                    FirstName = "Test4",
                    LastName = "User4"
                }
            };
            db.GetCollection<User>("test").InsertOne(user);
            db.GetCollection<User>("test").InsertMany(users);
            
            var res = db.GetCollection<User>("test").Find(u => u.Age > 30).ToList();
            res.ForEach(u => Console.WriteLine(u));
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
    }
}