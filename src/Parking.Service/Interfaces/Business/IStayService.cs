using Parking.Service.DTOs;

namespace Parking.Service.Interfaces.Business;

public interface IStayService : IDataRetrievalService<StayDTO>,
                                ICustomAddUpdateService<StayDTO>,
                                IDeleteService
{ }
