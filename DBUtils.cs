using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Shipul.Music;
using System.Data;
using ChoETL;

namespace Shipul.SqlConn
{
    class DBUtils
    {
        //Data Source=10.4.100.49;Initial Catalog=NAR;Persist Security Info=True;User ID=razuv;Password=***********
        //Data Source=10.4.100.37;Initial Catalog=profiserv;Persist Security Info=True;User ID=sysop;Password=***********
        public static SqlConnection GetDBConnection(string server)
        {
            string datasource;
            string database ;
            string username ;
            string password ;
            switch (server)
            {
                case "bufull":
                     datasource = @"10.4.100.37";
                     database = "profiserv";
                     username = "";
                     password = "";
                    break;
                case "safn":
                     datasource = @"10.4.100.49";
                     database = "NAR";
                     username = "";
                     password = "";
                     break;
                default :
                     Shipul.FileOperations.saveException(new Exception("сервер указан неверно"));
                     return null;
            }

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }

        // для nar.dbo.sp_test_ins
        public static void executeProcIns(SqlConnection connection, StoreProc proc, ModelExchange mE)
        {
            string sqlExpression = proc.sp_name;
            using (SqlCommand command = new SqlCommand(sqlExpression, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                /*foreach (var track in music.Tracks)
                {
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@album", track.Album));
                    command.Parameters.Add(new SqlParameter("@artist", track.Artist));
                    command.Parameters.Add(new SqlParameter("@title", track.Title));
                    command.Parameters.Add(new SqlParameter("@year", track.Year));
                    var result = command.ExecuteNonQuery();
                }
                 */ 
            }
        }



        // для nar.dbo.sp_test_ins
         public static void executeProcIns(SqlConnection connection,string proc , MyMusic music )
        {
            string sqlExpression = proc;
            using (SqlCommand command = new SqlCommand(sqlExpression, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var track in music.Tracks)
                {
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@album", track.Album));
                    command.Parameters.Add(new SqlParameter("@artist", track.Artist));
                    command.Parameters.Add(new SqlParameter("@title", track.Title));
                    command.Parameters.Add(new SqlParameter("@year", track.Year));
                    var result = command.ExecuteNonQuery();
                }
            }
         }

         //для nar.dbo.sp_test_read
         public static MyMusic executeProcSelect(SqlConnection connection, string proc)
         {
            string sqlExpression = proc;
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    MyMusic myCollection = new MyMusic();

                    while (reader.Read())
                    {

                        myCollection.Tracks.Add(new Track()
                        {
                            Album = reader.GetString(1),
                            Artist = reader.GetString(2),
                            Title = reader.GetString(3),
                            Year = reader.GetInt32(4)
                        });

                    }
                    reader.Close();
                    return myCollection;
                }
                
                return null;                
            }

        }



         //для nar.dbo.sp_test_read
         public static string  executeProcSelect(SqlConnection connection, StoreProc proc)
         {
             string sqlExpression = proc.sp_name;
             StringBuilder sb = new StringBuilder();
             SqlCommand command = new SqlCommand(sqlExpression, connection);
             command.CommandType = System.Data.CommandType.StoredProcedure;
             command.Parameters.Clear();

             DataTable dt = new DataTable();
             if (proc.prms != null)
             {
                 foreach (SpParam param in proc.prms)
                 {
                     command.Parameters.Add(new SqlParameter(param.param_name, param.prop_name));
                 }
             }
             using (SqlDataAdapter da = new SqlDataAdapter(command))
             {
                 da.Fill(dt);

                 using (var parser = new ChoJSONWriter(sb))
                     parser.Write(dt);
              
             }
             return sb.ToString();
         }

    }

}