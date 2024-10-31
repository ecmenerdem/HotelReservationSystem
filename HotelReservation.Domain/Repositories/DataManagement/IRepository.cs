using HotelReservation.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Repositories.DataManagement
{
    public interface IRepository<T> where T : AuditableEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, params string[] includeProperties);
        Task<T> GetByIdAsync(int ID);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params string[] includeProperties);

        Task<IQueryable<T>> GetAllAsyncAsNoTracking(Expression<Func<T, bool>> filter = null, params string[] includeProperties);
        ValueTask AddAsync(T entity);/*Insert Edilen Kaydı Geri Döndürmek İçin "void" yapmadık.*/
        void Update(T entity);
        void Remove(T entity);
    }
}
