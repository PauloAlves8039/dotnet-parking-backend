namespace Parking.Service.Interfaces;

public interface IService<TEntity, TDto> : IDataRetrievalService<TDto>, IAddService<TDto>, IUpdateService<TDto>, IDeleteService
    where TEntity : class
    where TDto : class
{ }
