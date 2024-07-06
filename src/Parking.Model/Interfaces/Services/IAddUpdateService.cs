namespace Parking.Model.Interfaces.Services;

public interface IAddUpdateService<TDto>
{
    Task<TDto> AddAsync(TDto dto);
    Task<TDto> UpdateAsync(TDto dto);

}
