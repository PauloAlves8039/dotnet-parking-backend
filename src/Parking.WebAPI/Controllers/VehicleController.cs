using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;

namespace Parking.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAll()
    {
        try
        {
            var vehicles = await _vehicleService.GetAllAsync();

            if (vehicles == null)
            {
                return NotFound("Vehicles not found.");
            }

            return Ok(vehicles);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleDTO>> GetById(int id)
    {
        try
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound($"Vehicle with code {id} not found.");
            }

            return Ok(vehicle);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDTO>> Post([FromBody] VehicleDTO vehicleDTO)
    {
        try
        {
            if (vehicleDTO == null)
            {
                return BadRequest("Vehicle data is null.");
            }

            var newVehicle = await _vehicleService.AddAsync(vehicleDTO);

            return CreatedAtAction(nameof(GetById), new { id = newVehicle.Id }, newVehicle);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] VehicleDTO vehicleDTO)
    {
        try
        {
            if (id != vehicleDTO.Id)
            {
                return BadRequest("Invalid vehicle data or mismatched IDs.");
            }

            await _vehicleService.UpdateAsync(vehicleDTO);

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
            var existingVehicle = await _vehicleService.GetByIdAsync(id);

            if (existingVehicle == null)
            {
                return NotFound();
            }

            await _vehicleService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
}
