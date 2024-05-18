namespace IncomeExpensesAccounting.DTO;

public record ClientDTO(int Id, string Name, string Phone, string Email, string Note): ClientAddDTO(Name, Phone, Email, Note);