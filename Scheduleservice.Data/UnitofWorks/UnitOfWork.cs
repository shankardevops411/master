using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Scheduleservice.Data.UnitofWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
            InitConnection();
        }

        IDbConnection _connection = null;
        IDbTransaction _transaction = null;

        private void InitConnection()
        {
            if (this._connection == null)
            {
                this._connection = new SqlConnection(this._connectionString);
            }

            if (this._connection.State == ConnectionState.Closed)
                this._connection.Open();
        }

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }
        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback(); 
            Dispose();
           
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;

            if (this._connection.State != ConnectionState.Closed)
                this._connection.Close();
        }
    }
}
