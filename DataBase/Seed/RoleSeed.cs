using DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Seed;

public class RoleSeed
{
    private readonly ModelBuilder _builder;

    public RoleSeed(ModelBuilder builder)
    {
        _builder = builder;
    }

    public void Seed()
    {
        _builder.Entity<Role>().HasData(
            new Role { Id = 1, Description = "Администратор" },
            new Role { Id = 2, Description = "Бухгалтер" }, 
            new Role { Id = 3, Description = "Владелец" });
    }
}