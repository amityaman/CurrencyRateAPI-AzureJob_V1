using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.Application.Dal
{
    /// <summary>
    /// Summary description for GetConnection.
    /// </summary>
    public class GetConnection
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

    }
}
