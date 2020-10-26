using System;
using System.Collections.Generic;
using p10_AdoNetDll;
using p10_DotNetDbTools;
using p10_EFdll;

namespace p10_DbMain
{
    class Program
    {
        static void Main(string[] args)
        {
            //EntityFrameworkDll databaseTool = new EntityFrameworkDll(Helper.GetConnectionString());
            AdoNetDll databaseTool = new AdoNetDll(Helper.GetConnectionString());
            Repository repo = new Repository(databaseTool);
            // repo.CreateUser(new User {FirstName = "Ivan", Login = "login1", Password = "asdf", LastName = "Ivanov"});
            // repo.CreateUser(new User {FirstName = "Sergey", Login = "log23", Password = "hfg", LastName = "Petrov"});
            // repo.CreateUser(new User {FirstName = "John", Login = "log333", Password = "dfg", LastName = "Dou"});
            
            // User user = repo.GetUserById(5);
            // Console.WriteLine(user?.FirstName);
            repo.UpdateUser(
                new User
                {
                    FirstName = "Romeo", Login = "log", Password = "123", Patronymic = "None", LastName = "Smith"
                }, 2);
            repo.DeleteUser(4);
        }
    }
}