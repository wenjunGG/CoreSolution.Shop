using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DapperCoreRepositoryBase
{
    public interface IMsSqlRepositoryBase<TEntity, TEntityDto>
    {
        #region 插入
        /// <summary>
        /// 插入
        /// </summary>
        void insert(TEntityDto t);
        #endregion

        #region  获取当个实体应用
        /// <summary>
        /// 根据ID 获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TEntityDto GetEntityDto(Guid Id);

        /// <summary>
        /// 根据Expression 获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TEntityDto GetEntityDto(Expression<Func<TEntity, bool>> where);

        #endregion

        #region 列表
        /// <summary>
        /// 列表带分页
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        List<TEntityDto> GetEntityDtoList<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> Order, Expression<Func<TEntity, TEntityDto>> Selector, out int Total, int PageIndex = 1, int PageSize = 10);

        /// <summary>
        /// 列表 不带分页
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        List<TEntityDto> GetEntityDtoList<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> Order, Expression<Func<TEntity, TEntityDto>> Selector);

        /// <summary>
        /// 列表 不带分页 不带selector
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        List<TEntityDto> GetEntityDtoList<TProperty>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TProperty>> Order);

        /// <summary>
        /// 列表 不带分页 不带selector 不带选择order
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        List<TEntityDto> GetEntityDtoList(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="Order">排序</param>
        /// <param name="Selector">查询字段</param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        List<TEntityDto> GetEntityDtoList();
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Update(TEntityDto t);

        #endregion
    }
}
