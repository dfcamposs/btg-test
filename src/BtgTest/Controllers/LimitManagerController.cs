using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BtgTest;

[Route("limit-manager")]
[Authorize]
public class LimitManagerController : ControllerBase
{
    private readonly ILimitManagerService _limitManagerService;

    public LimitManagerController(ILimitManagerService accountLimitService)
    {
        _limitManagerService = accountLimitService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Account-Branch")][Required] int branch,
        [FromHeader(Name = "X-Account-Number")][Required] int accountNumber
        )
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _limitManagerService.Get(branch, accountNumber);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody][Required] LimitManagerRequest limitManagerRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var accountLimitManager = limitManagerRequest.ToMap();
        var result = await _limitManagerService.Create(accountLimitManager);

        return CreatedAtAction("Get", new { accountLimitManager.Identifier }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody][Required] LimitManagerRequest limitManagerRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var accountLimitManager = limitManagerRequest.ToMap();

        var checkAccountLimit = await _limitManagerService.Get(accountLimitManager.Branch, accountLimitManager.AccountNumber);

        if (checkAccountLimit is null) return NotFound();

        var result = await _limitManagerService.Update(accountLimitManager);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromHeader(Name = "X-Account-Branch")][Required] int branch,
        [FromHeader(Name = "X-Account-Number")][Required] int accountNumber
        )
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _limitManagerService.Delete(branch, accountNumber);

        if (!result) return NotFound();

        return Ok();
    }

    [HttpPatch("reset-limit")]
    public async Task<IActionResult> Reset(
        [FromHeader(Name = "X-Account-Branch")][Required] int branch,
        [FromHeader(Name = "X-Account-Number")][Required] int accountNumber)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var accountLimit = await _limitManagerService.Get(branch, accountNumber);

        if (accountLimit is null) return NotFound();

        var result = await _limitManagerService.ResetLimit(accountLimit);

        return Ok(new LimitManagerResponse(true, result.CurrentPixLimit));
    }

    [HttpPatch("update-limit")]
    public async Task<IActionResult> UpdateLimit([FromBody][Required] ValidatePixLimitRequest validatePixLimitRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var accountLimit = await _limitManagerService.Get(validatePixLimitRequest.branch, validatePixLimitRequest.accountNumber);

        if (accountLimit is null) return NotFound();

        var result = await _limitManagerService.UpdateLimit(accountLimit, validatePixLimitRequest.pixTransactionAmount);

        return Ok(new LimitManagerResponse(true, result.CurrentPixLimit));
    }
}
