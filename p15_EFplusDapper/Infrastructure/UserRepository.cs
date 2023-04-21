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
                     left join Addresses a 
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

    public async Task<int> Create(User user)
    {
        //todo - add Adress model
        using IDbConnection db = new SqliteConnection(_conn);
        var sqlQuery =
            "insert into Users (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age); SELECT last_insert_rowid()";

        var userId = (await db.QueryAsync<int>(sqlQuery, user)).FirstOrDefault();

        var sqlAdress =
            "insert into Addresses (City, Street, FlatNumber, AdditionalInfo, UserId) Values(@City, @Street, @FlatNumber, @AdditionalInfo, @UserId)";

        
        foreach (var address in user.Address)
        {
            await db.QueryAsync(sqlAdress, new
            {
                City = address.City,
                Street = address.Street,
                FlatNumber = address.FlatNumBer,
                AdditionalInfo = address.AdditionalInfo,
                UserId = userId
            });
        }

        return userId;
    }
}