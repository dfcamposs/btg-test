namespace BtgTest;

public class ValidatePixLimitService : IValidatePixLimitService
{
  private readonly ILimitManagerRepository _limitManagerRepository;

  public ValidatePixLimitService(ILimitManagerRepository accountLimitRepository)
  {
    _limitManagerRepository = accountLimitRepository;
  }

  public bool ValidateTransaction(LimitManager accountLimitManager, decimal pixTransactionAmount)
  {
    return accountLimitManager.CurrentPixLimit >= pixTransactionAmount;
  }
}
