using DataBase;
using DataBase.Entity;
using IncomeExpensesAccounting.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpensesAccounting.Controllers;

[Route("roles")]
[Authorize(Policy = "AdministratorRole")]
public class RoleController(IncomeExpenseContext context) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DescriptionDTO>>> Get()
    {
        return await context.Roles.Select(x => new DescriptionDTO(x.Id, x.Description)).ToListAsync();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] DescriptionDTO contract)
    {
        var entity = await context.Roles.SingleOrDefaultAsync(x => x.Id == contract.Id);
        if (entity == null)
            return BadRequest("Не найдена сущность с таким id");

        entity.Description = contract.Description;
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] string contract)
    {
        var entity = new Role { Description = contract };

        await context.Roles.AddAsync(entity);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await context.Roles.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return BadRequest("Не найдена сущность с таким id");

        context.Roles.Remove(entity);
        await context.SaveChangesAsync();

        return Ok();
    }
}