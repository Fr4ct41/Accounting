using DataBase;
using DataBase.Entity;
using IncomeExpensesAccounting.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpensesAccounting.Controllers;

[Route("users")]
[Authorize(Policy = "AdministratorRole")]
public class UserController(IncomeExpenseContext context) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
    {
        return await context.Users.Select(x => new UserDTO(x.Id, x.Login, x.Password, x.RoleId)).ToListAsync();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UserDTO contract)
    {
        var entity = await context.Users.SingleOrDefaultAsync(x => x.Id == contract.Id);
        if (entity == null)
            return BadRequest("Не найден пользователь с таким id");
        entity.Login = contract.Login;
        entity.Password = contract.Password;
        entity.RoleId = contract.RoleId;
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] UserAddDTO contract)
    {
        var existedEntity = await context.Users.SingleOrDefaultAsync(x => x.Login == contract.Login);
        if(existedEntity != null)
            return BadRequest("Пользователь с таким логином уже существует");

        var entity = new User { Password = contract.Password, RoleId = contract.RoleId, Login = contract.Login };
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await context.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return BadRequest("Не найден пользователь с таким id");

        context.Users.Remove(entity);
        await context.SaveChangesAsync();

        return Ok();
    }
}