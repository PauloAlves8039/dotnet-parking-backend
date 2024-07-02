using AutoMapper;
using Parking.Model.Interfaces;
using Parking.Model.Models;
using Parking.Service.DTOs;
using Parking.Service.Interfaces.Business;

namespace Parking.Service.Services;

public class CustomerService : Service<Customer, CustomerDTO>, ICustomerService
{
    public CustomerService(IRepository<Customer> repository, IMapper mapper) : base(repository, mapper) { }
}
