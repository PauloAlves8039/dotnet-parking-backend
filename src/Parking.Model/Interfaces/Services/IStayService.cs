using Parking.Model.DTOs;

namespace Parking.Model.Interfaces.Services;

public interface IStayService : IDataRetrievalService<StayDTO>,
                                ICustomAddUpdateService<StayDTO>,
                                IDeleteService
{ }
