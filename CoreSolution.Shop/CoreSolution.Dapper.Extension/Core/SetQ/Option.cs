﻿using System;
using System.Data;
using System.Linq.Expressions;
using CoreSolution.Dapper.Extension.Core.Interfaces;

namespace CoreSolution.Dapper.Extension.Core.SetQ
{
    /// <summary>
    /// 配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Option<T> : Query<T>, IOption<T>
    {
        protected Option(IDbConnection conn, SqlProvider sqlProvider) : base(conn, sqlProvider)
        {

        }

        protected Option(IDbConnection conn, SqlProvider sqlProvider, IDbTransaction dbTransaction) : base(conn, sqlProvider, dbTransaction)
        {

        }

        /// <inheritdoc />
        public virtual Query<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            SqlProvider.SetContext.SelectExpression = selector;

            return new QuerySet<TResult>(DbCon, SqlProvider, typeof(T), DbTransaction);
        }

        /// <inheritdoc />
        public virtual Option<T> Top(int num)
        {
            SqlProvider.SetContext.TopNum = num;
            return this;
        }
    }
}
