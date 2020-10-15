namespace p10_DotNetDbTools
{
    public interface IDatabase
    {
        User GetUserById(int id);
        bool CreateUser(User user);
        bool DeleteUser(int id);
        bool UpdateUser(User user, int id);
    }
}