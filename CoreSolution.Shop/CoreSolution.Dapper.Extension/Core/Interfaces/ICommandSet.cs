﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreSolution.Dapper.Extension.Core.Interfaces
{
    public interface ICommandSet<T>
    {
        ICommand<T> Where(Expression<Func<T, bool>> predicate);

        IInsert<T> IfNotExists(Expression<Func<T, bool>> predicate);

        void BatchInsert(IEnumerable<T> entities, int timeout = 120);
    }
}
