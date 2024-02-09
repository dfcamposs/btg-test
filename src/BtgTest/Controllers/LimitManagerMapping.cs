namespace BtgTest;

public static class LimitManagerMapping
{
  public static LimitManager ToMap(this LimitManagerRequest limitManagerRequest)
  {
    return new LimitManager
    {
      Identifier = Guid.NewGuid(),
      DocumentNumber = limitManagerRequest.DocumentNumber,
      Branch = limitManagerRequest.Branch,
      AccountNumber = limitManagerRequest.AccountNumber,
      MaxPixLimit = limitManagerRequest.MaxPixLimit
    };
  }
}
