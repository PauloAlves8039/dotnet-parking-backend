using AutoMapper;
using Parking.Model.Interfaces;
using Parking.Model.Models;
using Parking.Service.DTOs;
using Parking.Service.Helpers.Interfaces;
using Parking.Service.Interfaces.Business;

namespace Parking.Service.Services;

public class StayService : IStayService
{
    private readonly IRepository<Stay> _repository;
    private readonly IMapper _mapper;
    private readonly IUtilities _utilities;

    public StayService(IRepository<Stay> repository, IMapper mapper, IUtilities utilities)
    {
        _repository = repository;
        _mapper = mapper;
        _utilities = utilities;
    }

    public async Task<IEnumerable<StayDTO>> GetAllAsync()
    {
        var stays = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<StayDTO>>(stays);
    }

    public async Task<StayDTO> GetByIdAsync(int id)
    {
        var stay = await _repository.GetByIdAsync(id);
        return _mapper.Map<StayDTO>(stay);
    }

    public async Task<StayDTO> AddAsync(StayDTO stayDTO)
    {
        var stay = _mapper.Map<Stay>(stayDTO);

        if (stayDTO.HourlyRate <= 0)
        {
            throw new ArgumentException("The hourly rate must be greater than zero when adding a stay.");
        }

        stay.HourlyRate = stayDTO.HourlyRate;

        await _repository.AddAsync(stay);
        await _utilities.UpdateStayStatus(stay.Id, "Estacionado", _repository);

        return stayDTO;
    }

    public async Task<StayDTO> UpdateAsync(StayDTO stayDTO)
    {
        stayDTO.ExitDate ??= DateTime.Now;

        var stayHours = _utilities.CalculateStayHours(stayDTO);
        var totalAmount = _utilities.CalculateTotalAmount(stayHours, stayDTO.HourlyRate);

        var stay = _mapper.Map<Stay>(stayDTO);

        stay.TotalAmount = totalAmount;

        await _repository.UpdateAsync(stay);
        await _utilities.UpdateStayStatus(stay.Id, "Retirado", _repository);

        return stayDTO;
    }

    public async Task DeleteAsync(int id)
    {
        var stay = await _repository.GetByIdAsync(id);

        if (stay == null)
        {
            throw new InvalidOperationException("Stays not found.");
        }
        await _repository.DeleteAsync(id);
    }
}
