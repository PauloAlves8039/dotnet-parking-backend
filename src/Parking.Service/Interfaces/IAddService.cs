namespace Parking.Service.Interfaces;

public interface IAddService<TDto>
{
    Task<TDto> AddAsync(TDto dto);
}
