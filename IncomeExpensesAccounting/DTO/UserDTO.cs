namespace IncomeExpensesAccounting.DTO;

public record UserDTO(int Id, string Login, string Password, int RoleId) : UserAddDTO(Login, Password, RoleId);