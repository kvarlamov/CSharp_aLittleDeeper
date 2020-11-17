using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Logic();
        }

        static void Logic()
        {
            client.BaseAddress = new Uri(@"http://localhost:57116/api");
            while (true)
            {
                Console.WriteLine("Print number of action and push 'ENTER' key");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1-Get user by id\n" +
                                  "2-Get all users\n" +
                                  "3-Create new user\n" +
                                  "4-Delete user\n");
                Console.ForegroundColor = ConsoleColor.White;
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            GetUser().Wait();
                            break;
                        case 2:
                            GetUsers().Wait();
                            break;
                        case 3:
                            CreateUser().Wait();
                            break;
                        case 4:
                            Console.WriteLine("Case 4");
                            break;
                    }
                    Console.WriteLine("0 - for exit, any key - try again");
                    if (!int.TryParse(Console.ReadLine(), out int key)) continue;
                    if (key == 0)
                        break;
                }
            }
        }

        private static async Task CreateUser()
        {
            Console.WriteLine("InsertUser");
            string userString = Console.ReadLine();
            try
            {
                HttpContent content = new StringContent(userString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}/users", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Something went wrong");
                Console.WriteLine(e);
            }
        }

        private static async Task GetUser()
        {
            Console.WriteLine("Input id of user");

            if (int.TryParse(Console.ReadLine(), out int parsedId))
            {
                await ResponseLogic($"users/{parsedId}/");
            }
            else
            {
                Console.WriteLine("Wrong id format. Try again\n");
            }
        }

        private static async Task GetUsers()
        {
            await ResponseLogic("users/");
        }

        private static async Task ResponseLogic(string postfix)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/{postfix}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Something went wrong");
                Console.WriteLine(e);
            }
        }
    }
}
