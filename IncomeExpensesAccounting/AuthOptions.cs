using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IncomeExpensesAccounting;

public class AuthOptions
{
    public const string ISSUER = "IncomeExpensesAccountingerver"; // издатель токена
    public const string AUDIENCE = "IncomeExpensesAccountingClient"; // потребитель токена
    const string KEY = "s8z.v=56%ie~5HqgEHl,>p9;DWu00a30";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}