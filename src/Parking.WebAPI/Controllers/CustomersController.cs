using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;

namespace Parking.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
    {
        try
        {
            var customers = await _customerService.GetAllAsync();

            if (customers == null)
            {
                return NotFound("Customers not found.");
            }

            return Ok(customers);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDTO>> GetById(int id)
    {
        try
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound($"Customer with code {id} not found.");
            }

            return Ok(customer);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> Post([FromBody] CustomerDTO customerDTO)
    {
        try
        {
            if (customerDTO == null)
            {
                return BadRequest("Customer data is null.");
            }

            var newCustomer = await _customerService.AddAsync(customerDTO);

            return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CustomerDTO customerDTO)
    {
        try
        {
            if (id != customerDTO.Id)
            {
                return BadRequest("Invalid Customer data or mismatched IDs.");
            }

            await _customerService.UpdateAsync(customerDTO);

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
            var existingCustomer = await _customerService.GetByIdAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
}
