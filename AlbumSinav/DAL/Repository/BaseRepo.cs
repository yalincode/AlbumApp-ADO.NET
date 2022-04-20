using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumSinav.DAL.Repository
{
    public abstract class BaseRepo
    {
        public BaseRepo()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        }

        private SqlConnection connection = null;

        public SqlConnection con
        {
            get { return connection; }

        }
    }
}
