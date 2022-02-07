using System;
using System.Linq.Expressions;

namespace Weelo.Abstrations.Specifications
{
    /// <summary>
    /// Specification pattern is implemented for the correct validation of entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T,bool>> Expression { get;}
        public bool IsSatisfiedBy(T entity)
        {
            Func<T,bool> func = Expression.Compile();
            return func(entity);
        }
    }
}
