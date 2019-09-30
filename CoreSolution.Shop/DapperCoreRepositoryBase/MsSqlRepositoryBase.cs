using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using CoreSolution.Dapper.Extension.MsSql;

namespace DapperCoreRepositoryBase
{
   public class MsSqlRepositoryBase<TEntity, TEntityDto>:DataBase, IMsSqlRepositoryBase<TEntity, TEntityDto>
    {
        protected IDbConnection _sqlCon;
        public MsSqlRepositoryBase()
        {
            this._sqlCon = base._sqlConnection;
        }

        #region 插入
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t"></param>
        public void insert(TEntityDto t)
        {
            base.CommandSet<TEntity>().Insert(Mapper.Map<TEntity>(t));
        }
        #endregion

        #region 获取单个实体
        /// <summary>
        /// 根据ID 获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntityDto GetEntityDto(Guid Id)
        {
           return (Mapper.Map<TEntityDto>(base.QuerySet<TEntity>().Where(CreateEqualityExpressionForId(Id)).Get()));
        }

        /// <summary>
        /// 根据Expression 获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntityDto GetEntityDto(Expression<Func<TEntity, bool>> where)
        {
            return (Mapper.Map<TEntityDto>(base.QuerySet<TEntity>().Where(where).Get()));
        }

        #endregion

        #region 列表
        /// <summary>
        /// 列表 查询条件 排序 带分页
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        public List<TEntityDto> GetEntityDtoList<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> Order, Expression<Func<TEntity, TEntityDto>> Selector, out int Total, int PageIndex=1,int PageSize = 10)
        {
            var updateResult6 = base.QuerySet<TEntity>().Where(where)
                .OrderBy(Order)
                .Select(Selector).PageList(PageIndex,PageSize);

            Total = updateResult6.Total;
            return (List<TEntityDto>)(updateResult6.Items);
        }

        /// <summary>
        /// 列表 不带分页
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <returns></returns>
        public List<TEntityDto> GetEntityDtoList<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> Order, Expression<Func<TEntity, TEntityDto>> Selector)
        {
            var updateResult6 = base.QuerySet<TEntity>().Where(where)
             .OrderBy(Order)
             .Select(Selector).ToList();
            
            return (List<TEntityDto>)(updateResult6);
        }

        /// <summary>
        /// 列表 不带分页 不带选择器
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <returns></returns>
        public List<TEntityDto> GetEntityDtoList<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> Order)
        {
            var updateResult6 = base.QuerySet<TEntity>().Where(where)
             .OrderBy(Order).ToList();

            return (List<TEntityDto>)(updateResult6);
        }

        /// <summary>
        /// 列表 不带分页 不带选择器 不带排序
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TEntityDto> GetEntityDtoList(Expression<Func<TEntity, bool>> where)
        {
            var updateResult6 = base.QuerySet<TEntity>().Where(where).ToList();

            return (List<TEntityDto>)(updateResult6);
        }

        /// <summary>
        ///  列表 单纯
        /// </summary>
        /// <returns></returns>
        public List<TEntityDto> GetEntityDtoList()
        {
            var updateResult6 = base.QuerySet<TEntity>().ToList();
            return (List<TEntityDto>)(updateResult6);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Update(TEntityDto t)
        {
          return base.CommandSet<TEntity>().Update(Mapper.Map<TEntity>(t));
        }

        #endregion



        #region 拓展
        /// <summary>
        /// 组建 ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(Guid Id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(Id, typeof(Guid))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        #endregion
    }
}
