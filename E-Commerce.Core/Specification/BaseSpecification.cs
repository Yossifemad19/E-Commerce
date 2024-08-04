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


    }
}
