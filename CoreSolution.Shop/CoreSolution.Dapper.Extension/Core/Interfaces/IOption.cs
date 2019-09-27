using System;
using System.Linq.Expressions;
using CoreSolution.Dapper.Extension.Core.SetQ;

namespace CoreSolution.Dapper.Extension.Core.Interfaces
{
    public interface IOption<T>
    {
        /// <summary>
        /// 选择器
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        Query<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// 前N条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        Option<T> Top(int num);
    }
}
