namespace Parking.Service.Interfaces;

public interface IUpdateService<TDto>
{
    Task<TDto> UpdateAsync(TDto dto);
}
