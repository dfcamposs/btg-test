using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BtgTest;

[Route("validate-limit")]
[Authorize]
public class ValidateLimitController : ControllerBase
{
  private readonly ILimitManagerService _limitManagerService;
  private readonly IValidatePixLimitService _validatePixLimitService;

  public ValidateLimitController(ILimitManagerService limitManagerService, IValidatePixLimitService validatePixLimitService)
  {
    _limitManagerService = limitManagerService;
    _validatePixLimitService = validatePixLimitService;
  }

  [HttpPost]
  public async Task<IActionResult> ValidateTransaction([FromBody][Required] ValidatePixLimitRequest validatePixLimitRequest)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var accountLimit = await _limitManagerService.Get(validatePixLimitRequest.branch, validatePixLimitRequest.accountNumber);

    if (accountLimit is null) return NotFound("Account Limit Configuration Not Found!");

    var result = _validatePixLimitService.ValidateTransaction(accountLimit, validatePixLimitRequest.pixTransactionAmount);

    return Ok(new LimitManagerResponse(result, accountLimit.CurrentPixLimit));
  }
}
