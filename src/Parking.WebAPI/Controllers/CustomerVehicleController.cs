using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;

namespace Parking.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class CustomerVehicleController : ControllerBase
{
    private readonly ICustomerVehicleService _customerVehicleService;

    public CustomerVehicleController(ICustomerVehicleService customerVehicleService)
    {
        _customerVehicleService = customerVehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerVehicleDTO>>> GetAll()
    {
        try
        {
            var customersVehicles = await _customerVehicleService.GetAllAsync();

            if (customersVehicles == null)
            {
                return NotFound("Customers Vehicles not found.");
            }

            return Ok(customersVehicles);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerVehicleDTO>> GetById(int id)
    {
        try
        {
            var customerVehicle = await _customerVehicleService.GetByIdAsync(id);

            if (customerVehicle == null)
            {
                return NotFound($"Customer Vehicle with code {id} not found.");
            }

            return Ok(customerVehicle);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CustomerVehicleDTO>> Post([FromBody] CustomerVehicleDTO customerVehicleDTO)
    {
        try
        {
            if (customerVehicleDTO == null)
            {
                return BadRequest("Customer Vehicle data is null.");
            }

            var newCustomerVehicle = await _customerVehicleService.AddAsync(customerVehicleDTO);

            return CreatedAtAction(nameof(GetById), new { id = newCustomerVehicle.Id }, newCustomerVehicle);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CustomerVehicleDTO customerVehicleDTO)
    {
        try
        {
            if (id != customerVehicleDTO.Id)
            {
                return BadRequest("Invalid Customer Vehicle data or mismatched IDs.");
            }

            await _customerVehicleService.UpdateAsync(customerVehicleDTO);

            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "DeletePermission")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingCustomerVehicle = await _customerVehicleService.GetByIdAsync(id);

            if (existingCustomerVehicle == null)
            {
                return NotFound();
            }

            await _customerVehicleService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
}
