using System.ComponentModel.DataAnnotations;

namespace BtgTest;

public class ValidatePixLimitRequest
{
  [Required]
  public int branch { get; set; }
  [Required]
  public int accountNumber { get; set; }
  [Required]
  public decimal pixTransactionAmount { get; set; }
}
