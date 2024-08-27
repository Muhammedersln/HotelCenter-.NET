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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
