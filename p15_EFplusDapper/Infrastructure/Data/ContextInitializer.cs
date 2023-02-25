using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace p15_EFplusDapper.Infrastructure.Data;

public class ContextInitializer
{
    public static void Initialize(DatabaseContext _db)
    {
        DatabaseFacade db = _db.Database;
        db.EnsureDeleted();
        db.EnsureCreated();
        
        _db.Users.AddRange(FakeDataFactory.GetUsers());

        _db.SaveChanges();
    }
}