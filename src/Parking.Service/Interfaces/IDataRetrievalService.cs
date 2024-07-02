namespace Parking.Service.Interfaces;

public interface IDataRetrievalService<TDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
}
