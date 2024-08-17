using E_Commerce.Core.Entities;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);

        public Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        public Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);

        public Task<int> CountAsync(ISpecification<T> spec);


        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
