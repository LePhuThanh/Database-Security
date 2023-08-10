using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    class DataConnection
    {
        string conStr = "Data Source=DESKTOP-H3P2P78\\PHUTHANH;Initial Catalog=QLSV;Integrated Security=True";
        public SqlConnection GetConnect()
        {
            return new SqlConnection(conStr);
        }
    }
}
