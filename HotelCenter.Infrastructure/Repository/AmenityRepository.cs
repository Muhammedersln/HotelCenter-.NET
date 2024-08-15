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
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public AmenityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Amenity entity)
        {
           _context.Amenities.Update(entity);
        }
    }
}
