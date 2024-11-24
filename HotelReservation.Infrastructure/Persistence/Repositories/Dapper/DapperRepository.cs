using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HotelReservation.Domain.Entities.Base;
using HotelReservation.Domain.Repositories.DataManagement;

namespace HotelReservation.Infrastructure.Persistence.Repositories.Dapper
{
    public class DapperRepository<T>: IRepository<T> where T: AuditableEntity 
    {
        private readonly IDbConnection _dbConnection;

        public DapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetAllAsync(Func<T, bool> filter = null, params string[] includeProperties)
        {
            string query = $"SELECT * FROM {typeof(T).Name}";
            return (await _dbConnection.QueryAsync<T>(query)).Where(filter);
        }

        public async Task<IQueryable<T>> GetAllAsyncAsNoTracking(Expression<Func<T, bool>> filter = null, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async ValueTask AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }
    }
   
}
