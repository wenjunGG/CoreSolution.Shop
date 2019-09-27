﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreSolution.Dapper.Extension.Core.Interfaces
{
    public interface ICommand<T>
    {
        int Update(T entity);

        Task<int> UpdateAsync(T entity);

        int Update(Expression<Func<T, T>> updateExpression);

        Task<int> UpdateAsync(Expression<Func<T, T>> updateExpression);

        int Delete();

        Task<int> DeleteAsync();
    }
}
