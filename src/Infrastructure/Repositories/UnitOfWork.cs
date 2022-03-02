﻿using Application.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAppUserRepository AppUsers { get; private set; }
        public IMapRepository Maps { get; private set; }
        public IOfficeRepository Offices { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IParkingPlaceRepository ParkingPlaces { get; private set; }
        public IVacationRepository Vacations { get; private set; }
        public IWorkPlaceRepository WorkPlaces { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
            IAppUserRepository appUsers,
            IMapRepository maps,
            IOfficeRepository offices,
            IBookingRepository bookings,
            IParkingPlaceRepository parkingPlaces,
            IVacationRepository vacations,
            IWorkPlaceRepository workPlaces)
        {
            _context = context;
            AppUsers = appUsers;
            Maps = maps;
            Offices = offices;
            Bookings = bookings;
            ParkingPlaces = parkingPlaces;
            Vacations = vacations;
            WorkPlaces = workPlaces;
        }
        

        public async Task <int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
