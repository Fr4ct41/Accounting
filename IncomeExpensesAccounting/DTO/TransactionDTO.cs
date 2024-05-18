namespace IncomeExpensesAccounting.DTO;

public record TransactionDTO(
    int Id,
    int UserId,
    int TransactionTypeId,
    decimal BuyAmount,
    decimal SellAmount,
    DateTime DateTime,
    int ClientId) : TransactionAddDTO(UserId, TransactionTypeId, BuyAmount, SellAmount, DateTime, ClientId);