namespace Parking.Service.Interfaces;

public interface ICustomAddUpdateService<TDto>
{
    Task<TDto> AddAsync(TDto dto);
    Task<TDto> UpdateAsync(TDto dto);
}
