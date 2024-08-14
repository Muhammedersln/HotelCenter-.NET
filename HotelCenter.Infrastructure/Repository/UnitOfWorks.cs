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

        public UnitOfWorks(ApplicationDbContext context)
        {
            _context = context;
            Hotel = new HotelRepository(_context);
            HotelNumber = new HotelNumberRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
