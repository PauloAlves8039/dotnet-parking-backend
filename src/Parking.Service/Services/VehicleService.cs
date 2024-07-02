﻿using AutoMapper;
using Parking.Model.Interfaces;
using Parking.Model.Models;
using Parking.Service.DTOs;
using Parking.Service.Interfaces.Business;

namespace Parking.Service.Services;

public class VehicleService : Service<Vehicle, VehicleDTO>, IVehicleService
{
    public VehicleService(IRepository<Vehicle> repository, IMapper mapper) : base(repository, mapper) { }
}