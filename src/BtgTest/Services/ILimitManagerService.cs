namespace BtgTest;

public interface ILimitManagerService
{
  Task<LimitManager?> Get(int branch, int accountNumber);

  Task<bool> Create(LimitManager accountLimitManager);

  Task<bool> Update(LimitManager accountLimitManager);

  Task<bool> Delete(int branch, int accountNumber);

  Task<LimitManager> ResetLimit(LimitManager accountLimitManager);

  Task<LimitManager> UpdateLimit(LimitManager accountLimitManager, decimal pixTransactionAmount);
}
