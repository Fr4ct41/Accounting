using DataBase;
using DataBase.Entity;
using IncomeExpensesAccounting.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpensesAccounting.Controllers;

[Route("clients")]
[Authorize(Policy = "AccountRole")]
public class ClientController(IncomeExpenseContext context) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> Get()
    {
        return await context.Clients.Select(x => new ClientDTO(x.Id, x.Name, x.Phone, x.Email, x.Note)).ToListAsync();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ClientDTO contract)
    {
        var entity = await context.Clients.SingleOrDefaultAsync(x => x.Id == contract.Id);
        if (entity == null)
            return BadRequest("Не найдена сущность с таким id");
        
        entity.Name = contract.Name;
        entity.Phone = contract.Phone;
        entity.Email = contract.Email;
        entity.Note = contract.Note;
        await context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ClientAddDTO contract)
    {
        var entity = new Client { Name = contract.Name, Phone = contract.Phone, Email = contract.Email, Note = contract.Note};
        
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await context.Clients.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return BadRequest("Не найдена сущность с таким id");

        context.Clients.Remove(entity);
        await context.SaveChangesAsync();

        return Ok();
    }
}