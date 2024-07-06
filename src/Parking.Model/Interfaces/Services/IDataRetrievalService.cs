namespace Parking.Model.Interfaces.Services;

public interface IDataRetrievalService<TDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
}
