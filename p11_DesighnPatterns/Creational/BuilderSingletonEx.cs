using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text.Json;

namespace p11_DesighnPatterns
{
    public class BuilderSingletonEx
    {
        public static void Test()
        {
            var user1Builder = new User.Builder(
                userId: 1, 
                firstName: "Ivan", 
                lastName: "Petrov", 
                age: 35);
            var user1 = user1Builder
                .SetHobby("Fishing")
                .SetHeight(175)
                .SetWeight(70)
                .SetChildren(Enumerable.Empty<User>())
                .SetCity("Moscow")
                .SetParameters(HtmlProvider.Instance.GetHtml("https://jsonplaceholder.typicode.com/todos/35"))
                .Build();

            var user2Builder = new User.Builder(
                userId: 2, 
                firstName: "Sergei", 
                lastName: "Pugachov", 
                age: 29);
            
            var user2 = user2Builder
                .SetHobby("Football")
                .SetHeight(185)
                .SetWeight(90)
                .SetChildren(Enumerable.Empty<User>())
                .SetCity("Moscow")
                .SetParameters(HtmlProvider.Instance.GetHtml("https://jsonplaceholder.typicode.com/todos/29"))
                .Build();

            var user3Builder = new User.Builder(
                userId: 3, 
                firstName: "Alexei", 
                lastName: "Konstantinov", 
                age: 35);
            
            var user3 = user3Builder
                .SetHobby("Chess")
                .SetHeight(182)
                .SetWeight(75)
                .SetChildren(Enumerable.Empty<User>())
                .SetCity("Moscow")
                .SetParameters(HtmlProvider.Instance.GetHtml("https://jsonplaceholder.typicode.com/todos/35"))
                .Build();
		
            Console.WriteLine($"\n\nUsers:\n\n{user1}\n\n{user2}\n\n{user3}");
        }
    }
    public class HtmlProvider
    {
        private static object _locker = new object();
        private static HtmlProvider _instance;
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        public static HtmlProvider Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (_locker)
                {
                    if (_instance == null)
                        _instance = new HtmlProvider();
                    return _instance;
                }
            }
        }

        public string GetHtml(string url)
        {
            if (!_cache.TryGetValue(url, out string html))
            {
                var request = WebRequest.Create(url);
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine($"Reading html at {url}");
                    html = reader.ReadToEnd();
                }
                _cache[url] = html;                
            }
            return html;
        }
    }

    public class User
    {
        private readonly int _userId;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly int _age;
        private readonly string _hobby;
        private readonly int _height;
        private readonly int _weight;
        private readonly IEnumerable<User> _children;
        private readonly string _city;
        private readonly string _parameters;
		
		public int UserId => _userId;
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public int Age => _age;
        public string Hobby => _hobby;
        public int Height => _height;
        public int Weight => _weight;
        public IEnumerable<User> Children => _children;
        public string City => _city;
        public string Parameters => _parameters;

        public class Builder
		{
            private readonly int _userId;
            private readonly string _firstName;
            private readonly string _lastName;
            private readonly int _age;
            private string _hobby;
            private int _height;
            private int _weight;
            private IEnumerable<User> _children;
            private string _city;
            private string _parameters;

            public Builder(int userId, string firstName, string lastName, int age)
            {
                _userId = userId;
                _firstName = firstName;
                _lastName = lastName;
                _age = age;
            }

            public User.Builder SetHobby(string hobby)
            {
                _hobby = hobby;
                return this;
            }
            
            public User.Builder SetHeight(int height)
            {
                _height = height;
                return this;
            }
            
            public User.Builder SetWeight(int weight)
            {
                _weight = weight;
                return this;
            }
            
            public User.Builder SetChildren(IEnumerable<User> children)
            {
                _children = children;
                return this;
            }
            
            public User.Builder SetCity(string city)
            {
                _city = city;
                return this;
            }
            
            public User.Builder SetParameters(string parameters)
            {
                _parameters = parameters;
                return this;
            }

            public User Build()
            {
                return new User(_userId, _firstName, _lastName, _age, _hobby, _height, _weight, _children, _city, _parameters);
            }
		}
		
		private User(int userId, string firstName, string lastName, int age, string hobby, int height, int weight, IEnumerable<User> children, string city, string parameters)
        {
            _userId = userId;
            _firstName = firstName;
            _lastName = lastName;
            _age = age;
            _hobby = hobby;
            _height = height;
            _weight = weight;
            _children = children;
            _city = city;
            _parameters = parameters;
        }
		
		public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}