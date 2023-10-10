using System;
using System.Collections.Generic;

namespace p23_ExpressionTrees
{
    class Program
    {
        private static List<User> _users;
        
        static void Main(string[] args)
        {
            _users = GenerateRandomUsers(20);
            Console.WriteLine("Hello World!");
        }

        static List<User> FilterUsers()
        {
            throw new NotImplementedException();
        }
        
        static List<User> GenerateRandomUsers(int count)
        {
            var users = new List<User>();
            Random random = new Random();

            // Pre-defined lists of first names, last names, and addresses
            string[] firstNames = { "John", "Mary", "David", "Sarah", "Michael", "Emily", "James", "Emma", "Daniel", "Olivia" };
            string[] lastNames = { "Smith", "Johnson", "Brown", "Davis", "Jones", "Wilson", "Miller", "Taylor", "Anderson", "Jackson" };
            string[] addresses = { "123 Main St", "456 Elm St", "789 Oak St", "101 Maple Ave", "222 Pine Rd", "333 Cedar Ln", "444 Birch Ct", "555 Walnut Dr", "666 Spruce Blvd", "777 Cherry Pl" };

            for (int i = 0; i < count; i++)
            {
                User user = new User
                {
                    Id = i + 1,
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    Age = random.Next(18, 65),
                    Weight = random.Next(50, 100),
                    Height = random.Next(150, 200),
                    BirthDayDate = DateTime.Now.AddYears(-random.Next(18, 50)),
                    Address1 = addresses[random.Next(addresses.Length)],
                    Gender = (Gender)random.Next(2)
                };

                users.Add(user);
            }

            return users;
        }
    }
}