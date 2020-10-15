using System;
using p10_DotNetDbTools;
using p10_EFdll;

namespace p10_DbMain
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityFrameworkDll databaseTool = new EntityFrameworkDll(Helper.GetConnectionString());
            Repository repo = new Repository(databaseTool);
            repo.CreateUser(new User {FirstName = "Иван", Login = "123", Password = "456", LastName = "Иванов"});
            repo.CreateUser(new User {FirstName = "Сергей", Login = "224", Password = "456", LastName = "Петров"});
            repo.CreateUser(new User {FirstName = "Савелий", Login = "323", Password = "456", LastName = "Сергеев"});
            
            User user = repo.GetUserById(1);
            Console.WriteLine(user?.FirstName);
            repo.UpdateUser(new User {FirstName = "Роман"}, 2);
            repo.DeleteUser(1);
        }
    }
}