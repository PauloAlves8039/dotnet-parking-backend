using AutoMapper;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Interfaces.Services;
using Parking.Model.Models;

namespace Parking.Service.Services;

public class CustomerService : Service<Customer, CustomerDTO>, ICustomerService
{
    public CustomerService(IRepository<Customer> repository, IMapper mapper) : base(repository, mapper) { }
}
