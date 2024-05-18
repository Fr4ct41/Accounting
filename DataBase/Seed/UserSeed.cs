using DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Seed;

public class UserSeed
{
    private readonly ModelBuilder _builder;

    public UserSeed(ModelBuilder builder)
    {
        _builder = builder;
    }

    public void Seed()
    {
        _builder.Entity<User>().HasData(
            new User { Id = 1, Login = "111", Password = "123", RoleId = 1 },
            new User { Id = 2, Login = "222", Password = "123", RoleId = 2 },
            new User { Id = 3, Login = "333", Password = "123", RoleId = 3 });
    }
}