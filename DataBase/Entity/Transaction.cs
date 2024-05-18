using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entity;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int TransactionTypeId { get; set; }
    
    public decimal BuyAmount { get; set; }
    
    public decimal SellAmount { get; set; }
    
    [Required]
    public DateTime DateTime { get; set; }
    
    [Required]
    public int ClientId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    
    [ForeignKey(nameof(TransactionTypeId))]
    public TransactionType TransactionType { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}