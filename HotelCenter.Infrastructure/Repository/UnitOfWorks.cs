using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelCenter.Infrastructure.Repository
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IHotelRepository Hotel { get; private set; }
        public IHotelNumberRepository HotelNumber { get; private set; }
        public IAmenityRepository Amenity { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public IApplicationUserRepository User { get; private set; }

        public UnitOfWorks(ApplicationDbContext context)
        {
            _context = context;
            Hotel = new HotelRepository(_context);
            Amenity = new AmenityRepository(_context);
            HotelNumber = new HotelNumberRepository(_context);
            Booking = new BookingRepository(_context);
            User = new ApplicationUserRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
