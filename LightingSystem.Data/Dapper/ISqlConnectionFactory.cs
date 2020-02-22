using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LightingSystem.Data.Dapper
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
