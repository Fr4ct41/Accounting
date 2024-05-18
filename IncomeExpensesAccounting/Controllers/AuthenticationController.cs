using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DataBase;
using DataBase.Entity;
using IncomeExpensesAccounting.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IncomeExpensesAccounting.Controllers;

[Route("Authentication")]
public class AuthenticationController(IncomeExpenseContext context) : Controller
{
    [HttpPost]
    public async Task<ActionResult> LogIn([FromBody] LoginDTO contract)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Login == contract.Login);
        if (user == null)
            return BadRequest("Не найдена сущность с таким логином");

        var isSuccess = user.Password == contract.Password;

        context.LoginHistories.Add(new LoginHistory()
            { DateTime = DateTime.UtcNow, IsSuccess = isSuccess, UserId = user.Id });
        await context.SaveChangesAsync();

        if (!isSuccess)
        {
            return BadRequest("Неправильный пароль");
        }

        var role = await context.Roles.SingleOrDefaultAsync(x => x.Id == user.RoleId);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, role.Description),
            new(ClaimTypes.NameIdentifier, user.Login)
        };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}