﻿using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        private readonly DbSet<T> _dbSet;   

        public GenericRepository(StoreContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            var query=ApplySpecification(spec);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            var query =ApplySpecification(spec);
            return await query.FirstOrDefaultAsync();
        }



        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}
