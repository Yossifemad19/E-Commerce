using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T,bool>> Criteria { get; }

    

        public List<Expression<Func<T, object>>> Includes { get; } =new List<Expression<Func<T,object>>>();
        public Expression<Func<T, object>> OrderBy { get ;private set ; }
        public Expression<Func<T, object>> OrderByDesc { get;private set; }

        public int Skip { get;private set; }
        public int Take { get;private set; }
        public bool IsPagingEnabled { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        public void AddInclude(Expression<Func<T,object>> includeExpression) {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T,object>> orderByExpression)
        {
            OrderBy=orderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T,object>> orderByDescExpression) {
            OrderByDesc=orderByDescExpression;
        }

        protected void ApplyPagination(int skip,int take)
        {
            Skip=skip;
            Take=take;
            IsPagingEnabled=true;
        }

    }
}
