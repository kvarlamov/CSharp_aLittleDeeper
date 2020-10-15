using System;
using System.Linq;
using p10_DotNetDbTools;

namespace p10_EFdll
{
    public class EntityFrameworkDll: IDatabase
    {
        private readonly string _connectionString;

        public EntityFrameworkDll(string connectionString)
        {
            _connectionString = connectionString;
        }
    
        public User GetUserById(int id)
        {
            using (ApplicationContext db = new ApplicationContext(_connectionString))
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public bool CreateUser(User user)
        {
            User newUser = user;
            using (ApplicationContext db = new ApplicationContext(_connectionString))
            {
                db.Users.Add(newUser);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public bool DeleteUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext(_connectionString))
            {
                User user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        //no action
                    }
                }
                return false;
            }
        }

        public bool UpdateUser(User user, int id)
        {
            using (ApplicationContext db = new ApplicationContext(_connectionString))
            {
                User userFromDb = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    userFromDb.City = user.City;
                    userFromDb.Login = user.Login;
                    userFromDb.Password = user.Password;
                    userFromDb.FirstName = user.FirstName;
                    userFromDb.LastName = user.LastName;
                    userFromDb.Patronymic = user.Patronymic;

                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        //no action
                    }
                }

                return false;
            }
        }
    }
}