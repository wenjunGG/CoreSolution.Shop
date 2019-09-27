using System;
using System.Data;
using System.Data.SqlClient;
using CoreSolution.Dapper.Extension.Core.SetC;
using CoreSolution.Dapper.Extension.Core.SetQ;
using CoreSolution.Dapper.Extension.Model;

namespace CoreSolution.Dapper.Extension.MsSql
{
    public class DataBase
    {
        protected IDbConnection _sqlConnection;
       
        protected DataBase()
        {
            this._sqlConnection = new SqlConnection("Data Source=192.168.1.44\\sqlserver;Initial Catalog=SkyJun;Persist Security Info=True;User ID=sa;Password=reload");
        }

        public QuerySet<T> QuerySet<T>()
        {
            return new QuerySet<T>(_sqlConnection, new MsSqlProvider());
        }

        public QuerySet<T> QuerySet<T>(IDbTransaction dbTransaction)
        {
            return new QuerySet<T>(_sqlConnection, new MsSqlProvider(), dbTransaction);
        }
        
        public CommandSet<T> CommandSet<T>(IDbTransaction dbTransaction)
        {
            return new CommandSet<T>(_sqlConnection, new MsSqlProvider(), dbTransaction);
        }

        public  CommandSet<T> CommandSet<T>()
        {
            return new CommandSet<T>(_sqlConnection, new MsSqlProvider());
        }

        public  void Transaction(Action<TransContext> action,
            Action<System.Exception> exAction = null)
        {
            if (_sqlConnection.State == ConnectionState.Closed)
                _sqlConnection.Open();

            var transaction = _sqlConnection.BeginTransaction();
            try
            {
                action(new TransContext(_sqlConnection, transaction, new MsSqlProvider()));
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                if (exAction != null)
                    exAction(ex);
                else
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
