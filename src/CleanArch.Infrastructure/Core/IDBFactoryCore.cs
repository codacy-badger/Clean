using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleanArch.Infrastructure.Core
{
    public interface IDBFactoryCore
    {
        string GetConnectionString(DBConnectionMode dbConnectionMode);
    }
}
