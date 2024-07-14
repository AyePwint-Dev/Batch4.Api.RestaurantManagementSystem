using Batch4.Api.RestaurantManagementSystem.BL.Services.Tax;

namespace Batch4.Api.RestaurantManagementSystem.Api.Controllers.Tax;

[Route("api/[controller]")]
[ApiController]
public class TaxController : ControllerBase
{
    private readonly BL_Tax _blTax;

    public TaxController(BL_Tax blTax)
    {
        _blTax = blTax;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTax(TaxRequestModel tax)
    {
        try
        {
            var result = await _blTax.CreateTax(tax);
            string message = result > 0 ? "New Tax Creation Successful" : "New Tax Creation Fail";
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTax()
    {
        try
        {
            var list = await _blTax.GetAllTax();
            return Ok(list);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var item = await _blTax.GetTaxById(id);
            return Ok(item);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByTaxCode(string code)
    {
        try
        {
            var item = await _blTax.GetTaxByTaxCode(code);
            if (item is null) return Ok("No Tax Found.");
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _blTax.DeleteTax(id);
            string message = result > 0 ? "Deleted Successful" : "Failed delete!";
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
