using System;
using System.Collections.Generic;
using System.Text;

namespace DapperCoreRepositoryBase
{
    public interface IMsSqlRepositoryBase<TEntity, TEntityDto>
    {
        /// <summary>
        /// 插入
        /// </summary>
        void insert(TEntityDto t);
    }
}
