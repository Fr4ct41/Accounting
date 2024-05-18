using DataBase;
using DataBase.Entity;
using IncomeExpensesAccounting.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IncomeExpensesAccounting.Controllers;

[Route("transactions")]
[Authorize(Policy = "AccountRole")]
public class TransactionController(IncomeExpenseContext context) : Controller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDTO>>> Get()
    {
        return await context.Transactions.Select(x =>
                new TransactionDTO(x.Id, x.UserId, x.TransactionTypeId, x.BuyAmount, x.SellAmount, x.DateTime,
                    x.ClientId))
            .ToListAsync();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] TransactionDTO contract)
    {
        var entity = await context.Transactions.SingleOrDefaultAsync(x => x.Id == contract.Id);
        if (entity == null)
            return BadRequest("Не найдена сущность с таким id");

        entity.UserId = contract.UserId;
        entity.TransactionTypeId = contract.TransactionTypeId;
        entity.BuyAmount = contract.BuyAmount;
        entity.SellAmount = contract.SellAmount;
        entity.DateTime = contract.DateTime;
        entity.ClientId = contract.ClientId;

        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] TransactionAddDTO contract)
    {
        var entity = new Transaction
        {
            UserId = contract.UserId, TransactionTypeId = contract.TransactionTypeId, BuyAmount = contract.BuyAmount,
            SellAmount = contract.SellAmount, DateTime = contract.DateTime, ClientId = contract.ClientId
        };
        await context.Transactions.AddAsync(entity);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await context.Transactions.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return BadRequest("Не найдена сущность с таким id");

        context.Transactions.Remove(entity);
        await context.SaveChangesAsync();

        return Ok();
    }
}