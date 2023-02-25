using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using p15_EFplusDapper.Infrastructure.Entities;

namespace p15_EFplusDapper.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly string _conn;

    public UserRepository(string conn)
    {
        _conn = conn;
    }
    
    public async Task<User> GetById(long id)
    {
        using IDbConnection db = new SqliteConnection(_conn);
        var sql = @"
            select * from Users u 
                     inner join Addresses a 
                     on u.Id = a.UserId
                     where u.Id=@id";
        var res = await db.QueryAsync<User, Address, User>(sql, (user, address) =>
        {
            user.Address = new List<Address>()
            {
                address
            };

            return user;
        }, new {id});
        
        return res.FirstOrDefault();
    }

    public async Task<List<User>> GetAll()
    {
        using IDbConnection db = new SqliteConnection(_conn);
        var sql = @"
            select * from Users u 
                     inner join Addresses a 
                     on u.Id = a.UserId";
        var res = await db.QueryAsync<User, Address, User>(sql, (user, address) =>
        {
            user.Address = new List<Address>()
            {
                address
            };

            return user;
        });
        
        return res.ToList();
    }
}