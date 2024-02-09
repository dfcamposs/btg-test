namespace BtgTest;

public interface IValidatePixLimitService
{
  bool ValidateTransaction(LimitManager accountLimitManager, decimal pixTransactionAmount);
}
