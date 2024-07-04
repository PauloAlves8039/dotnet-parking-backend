using Microsoft.AspNetCore.Mvc;
using Parking.Service.DTOs;
using Parking.Service.Interfaces.Business;

namespace Parking.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StayController : ControllerBase
{
    private readonly IStayService _stayService;

    public StayController(IStayService stayService)
    {
        _stayService = stayService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StayDTO>>> GetAll()
    {
        try
        {
            var stays = await _stayService.GetAllAsync();

            if (stays == null)
            {
                return NotFound("Stays not found.");
            }

            return Ok(stays);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StayDTO>> GetById(int id)
    {
        try
        {
            var stay = await _stayService.GetByIdAsync(id);

            if (stay == null)
            {
                return NotFound($"Stay with code {id} not found.");
            }

            return Ok(stay);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<AddressDTO>> Post([FromBody] StayDTO stayDTO)
    {
        try
        {
            if (stayDTO == null)
            {
                return BadRequest("Stay data is null.");
            }

            var newStay = await _stayService.AddAsync(stayDTO);

            return CreatedAtAction(nameof(GetById), new { id = newStay.Id }, newStay);
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] StayDTO stayDTO)
    {
        try
        {
            if (id != stayDTO.Id)
            {
                return BadRequest("Invalid Stay data or mismatched IDs.");
            }

            await _stayService.UpdateAsync(stayDTO);

            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingStay = await _stayService.GetByIdAsync(id);

            if (existingStay == null)
            {
                return NotFound();
            }

            await _stayService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception exception)
        {
            return StatusCode(500, $"Internal server error: {exception.Message}");
        }
    }
}
