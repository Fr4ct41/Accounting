using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Entity;

public class LoginHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public DateTime DateTime { get; set; }
    
    [Required]
    public bool IsSuccess { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}