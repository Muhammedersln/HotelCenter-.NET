using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelCenter.Domain.Entities;

namespace HotelCenter.Application.Common.Interfaces
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAll(Expression<Func<Hotel, bool>>? filter = null, string? includeProperties = null);
        IEnumerable<Hotel> Get(Expression<Func<Hotel, bool>> filter, string? includeProperties = null);
        void Add(Hotel entity);
        void Update(Hotel entity);
        void Remove(Hotel entity);
        void Save();
    }
}
