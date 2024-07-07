using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;

namespace Parking.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAll()
    {
        try
        {
            var adresses = await _addressService.GetAllAsync();

            if (adresses == null)
            {
                return NotFound("Adresses not found.");
            }

            return Ok(adresses);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AddressDTO>> GetById(int id)
    {
        try
        {
            var address = await _addressService.GetByIdAsync(id);

            if (address == null)
            {
                return NotFound($"Address with code {id} not found.");
            }

            return Ok(address);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<AddressDTO>> Post([FromBody] AddressDTO addressDTO)
    {
        try
        {
            if (addressDTO == null)
            {
                return BadRequest("Address data is null.");
            }

            var newAddress = await _addressService.AddAsync(addressDTO);

            return CreatedAtAction(nameof(GetById), new { id = newAddress.Id }, newAddress);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] AddressDTO addressDTO)
    {
        try
        {
            if (id != addressDTO.Id)
            {
                return BadRequest("Invalid Address data or mismatched IDs.");
            }

            await _addressService.UpdateAsync(addressDTO);

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
            var existingAddress = await _addressService.GetByIdAsync(id);

            if (existingAddress == null)
            {
                return NotFound();
            }

            await _addressService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
}
