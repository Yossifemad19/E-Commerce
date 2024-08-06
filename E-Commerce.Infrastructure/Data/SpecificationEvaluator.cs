using E_Commerce.Core.Entities;
using E_Commerce.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data
{
    public static class SpecificationEvaluator<T>where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> queryInput,ISpecification<T> spec)
        {
            var query = queryInput;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            if(spec.OrderBy is not null) {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            if(spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query,(current,include)
                =>current.Include(include)
                );

            return query;
        }
    }
}
