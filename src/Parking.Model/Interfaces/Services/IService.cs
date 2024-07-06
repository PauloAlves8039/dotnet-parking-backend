namespace Parking.Model.Interfaces.Services;

public interface IService<TEntity, TDto> : IDataRetrievalService<TDto>, IAddUpdateService<TDto>, IDeleteService
    where TEntity : class
    where TDto : class
{ }
