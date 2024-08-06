using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T,bool>> Criteria { get; }
        public List<Expression<Func<T,object>>> Includes { get; }

        public Expression<Func<T,object>> OrderBy { get;  }

        public Expression<Func<T, object>> OrderByDesc { get;  }
        public int Skip { get; }
        public int Take { get; }
        public bool IsPagingEnabled { get; }


    }
}
