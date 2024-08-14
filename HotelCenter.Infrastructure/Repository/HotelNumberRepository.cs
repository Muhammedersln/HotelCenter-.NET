using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelCenter.Application.Common.Interfaces;
using HotelCenter.Domain.Entities;
using HotelCenter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelCenter.Infrastructure.Repository
{
    public class HotelNumberRepository : Repository<HotelNumber>, IHotelNumberRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelNumberRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(HotelNumber entity)
        {
           _context.HotelNumbers.Update(entity);
        }
    }
}
