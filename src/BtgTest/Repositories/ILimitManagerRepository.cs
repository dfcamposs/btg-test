namespace BtgTest;

public interface ILimitManagerRepository
{
    Task<bool> Put(LimitManager accountLimitManager);

    Task<LimitManager?> Get(int branch, int accountNumber);

    Task<bool> Delete(int branch, int accountNumber);
}
