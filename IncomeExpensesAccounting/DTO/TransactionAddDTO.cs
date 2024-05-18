namespace IncomeExpensesAccounting.DTO;

public record TransactionAddDTO(
    int UserId,
    int TransactionTypeId,
    decimal BuyAmount,
    decimal SellAmount,
    DateTime DateTime,
    int ClientId);