using System;
using System.Linq.Expressions;
using CoreSolution.Dapper.Extension.Core.SetQ;

namespace CoreSolution.Dapper.Extension.Core.Interfaces
{
    public interface IQuerySet<T>
    {
        QuerySet<T> Where(Expression<Func<T, bool>> predicate);

        QuerySet<T> WithNoLock();
    }
}
