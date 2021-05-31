using System;
using System.Data;

namespace Persistence.Dapper
{
    public class FactoryConnection : IFactoryConnection
    {
        private IDbConnection _connection;

        public FactoryConnection(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
