using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public void insert(TEntityDto t)
        {
            base.CommandSet<TEntity>().Insert(Mapper.Map<TEntity>(t));
        }
    }
}
