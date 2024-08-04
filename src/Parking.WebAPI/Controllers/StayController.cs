using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Services;
using Parking.Service.Helpers.Interfaces;

namespace Parking.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class StayController : ControllerBase
{
    private readonly IStayService _stayService;
    private readonly IPdfService _pdfService;

    public StayController(IStayService stayService, IPdfService pdfService = null)
    {
        _stayService = stayService;
        _pdfService = pdfService;
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

    [HttpGet("ticket/{id:int}")]
    public async Task<IActionResult> GeneratePdf(int id)
    {
        try
        {
            var stayDto = await _stayService.GetByIdAsync(id);

            if (stayDto == null)
            {
                return NotFound($"Stay with code {id} not found.");
            }

            if (_pdfService == null)
            {
                return StatusCode(500, "PDF service is not available.");
            }

            var pdfBytes = _pdfService.GenerateStayPdf(stayDto);
            return File(pdfBytes, "application/pdf", "Stay.pdf");
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
    [Authorize(Policy = "DeletePermission")]
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
