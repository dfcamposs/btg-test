using System.ComponentModel.DataAnnotations;

namespace BtgTest;

public class LimitManagerRequest
{
  [Required]
  public string? DocumentNumber { get; set; }
  [Required]
  public int Branch { get; set; }
  [Required]
  public int AccountNumber { get; set; }
  [Required]
  public decimal MaxPixLimit { get; set; }
}
