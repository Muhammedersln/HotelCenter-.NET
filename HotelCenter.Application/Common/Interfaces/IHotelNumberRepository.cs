using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelCenter.Domain.Entities;

namespace HotelCenter.Application.Common.Interfaces
{
    public interface IHotelNumberRepository : IRepository<HotelNumber>
    {
       
        void Update(HotelNumber entity);
       
    }
}
