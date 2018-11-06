using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Shipul.SqlConn;
using Shipul.Watcher;

namespace ConnectSQLServer
{
    class Program
    {
       

        static void Main(string[] args)
        {
             string path = "D:/c#";
             string filter = "*.flg";


           // SqlConnection conn = DBUtils.GetDBConnection(DBSQLServerUtils.SERVER);
            try
            {

                    Console.WriteLine("Подключение настроено на : " + DBSQLServerUtils.SERVER);
                    MyWatcher watcher = new MyWatcher(path, filter);
                    watcher.Run();

                
            }
            catch (Exception e)
            {
                Shipul.FileOperations.saveException(e);
            }
           

            Console.ReadKey();
            
        }


       



    }

}