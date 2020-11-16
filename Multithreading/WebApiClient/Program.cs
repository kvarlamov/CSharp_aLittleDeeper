using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            GetUser();
        }

        private static void GetUser()
        {
            client.BaseAddress = new Uri(@"http://localhost:5000/api");
            while (true)
            {
                Console.WriteLine("Input id of user");

                if (int.TryParse(Console.ReadLine(), out int parsedId))
                {
                    HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/users/{parsedId}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        Console.WriteLine(response);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Wrong id format. Try again\n");
                }
            }
        }
    }
}
