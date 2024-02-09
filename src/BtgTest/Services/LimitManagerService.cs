namespace BtgTest;

public class LimitManagerService : ILimitManagerService
{
  private readonly ILimitManagerRepository _limitManagerRepository;

  public LimitManagerService(ILimitManagerRepository accountLimitRepository)
  {
    _limitManagerRepository = accountLimitRepository;
  }

  public async Task<LimitManager?> Get(int branch, int accountNumber)
  {
    return await _limitManagerRepository.Get(branch, accountNumber);
  }

  public async Task<bool> Create(LimitManager accountLimitManager)
  {
    accountLimitManager.CreatedAt = DateTime.UtcNow;
    return await _limitManagerRepository.Put(accountLimitManager);
  }

  public async Task<bool> Update(LimitManager accountLimitManager)
  {
    accountLimitManager.UpdatedAt = DateTime.UtcNow;
    return await _limitManagerRepository.Put(accountLimitManager);
  }

  public async Task<bool> Delete(int branch, int accountNumber)
  {
    return await _limitManagerRepository.Delete(branch, accountNumber);
  }

  public async Task<LimitManager> ResetLimit(LimitManager accountLimitManager)
  {
    accountLimitManager.CurrentPixLimit = accountLimitManager.MaxPixLimit;
    accountLimitManager.UpdatedAt = DateTime.UtcNow;

    await _limitManagerRepository.Put(accountLimitManager);

    return accountLimitManager;
  }

  public async Task<LimitManager> UpdateLimit(LimitManager accountLimitManager, decimal pixTransactionAmount)
  {
    accountLimitManager.CurrentPixLimit -= pixTransactionAmount;
    accountLimitManager.UpdatedAt = DateTime.UtcNow;

    await _limitManagerRepository.Put(accountLimitManager);

    return accountLimitManager;
  }
}
