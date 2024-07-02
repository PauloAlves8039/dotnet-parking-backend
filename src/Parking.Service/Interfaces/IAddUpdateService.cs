namespace Parking.Service.Interfaces;

public interface IAddUpdateService<TDto>
{
    Task<TDto> AddAsync(TDto dto);
    Task<TDto> UpdateAsync(TDto dto);

}
