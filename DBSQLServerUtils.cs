using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Shipul.SqlConn
{
    class DBSQLServerUtils
    {
        public const string SERVER ="bufull" ;

        public static SqlConnection
                 GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //Data Source=10.4.100.37;Initial Catalog=profiserv;Persist Security Info=True;User ID=sysop;Password=***********
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            try
            {
                SqlConnection conn = new SqlConnection(connString);
                return conn;
            }
            catch(SqlException e)
            {
                Shipul.FileOperations.saveException(e);
            }
            return null;
            
        }


    }
}