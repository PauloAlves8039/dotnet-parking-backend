namespace Parking.Model.Interfaces.Services;

public interface IDeleteService
{
    Task DeleteAsync(int id);
}
