namespace BtgTest;

public class LimitManagerResponse
{
  private readonly bool Approved;
  private readonly decimal CurrentPixLimit;

  public LimitManagerResponse(bool approved, decimal currentPixLimit)
  {
    Approved = approved;
    CurrentPixLimit = currentPixLimit;
  }
}
