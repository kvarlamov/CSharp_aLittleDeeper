namespace p10_DotNetDbTools
{
    public class Repository
    {
        private IDatabase _db;
        public Repository(IDatabase db)
        {
            _db = db;
        }

        public User GetUserById(int id)
        {
            return _db.GetUserById(id);
        }

        public bool UpdateUser(User user, int id)
        {
            return _db.UpdateUser(user, id);
        }

        public bool CreateUser(User user)
        {
            return _db.CreateUser(user);
        }

        public bool DeleteUser(int id)
        {
            return _db.DeleteUser(id);
        }
    }
}