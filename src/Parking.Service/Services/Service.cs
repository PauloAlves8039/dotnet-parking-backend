using AutoMapper;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Interfaces.Services;

namespace Parking.Service.Services;

public class Service<TEntity, TDto> : IService<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    protected readonly IRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public Service(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public virtual async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> AddAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
        return dto;
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.UpdateAsync(entity);
        return dto;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new InvalidOperationException("Entity not found.");
        }
        await _repository.DeleteAsync(id);
    }
}
