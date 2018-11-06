using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Shipul;
using Shipul.Music;
using Shipul.SqlConn;

namespace Shipul.Watcher
{
    class MyWatcher
    {
        FileSystemWatcher fsw;
        ExchangeFiles exF ;
        public MyWatcher(string path, string filter)
        {
            fsw = new FileSystemWatcher(path, filter);
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.Created += new FileSystemEventHandler(fsw_Changed);
            fsw.Deleted += new FileSystemEventHandler(fsw_Changed);           
            fsw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;  // NotifyFilters.LastAccess |
            exF = new ExchangeFiles();
            exF.Operations.Add(new Operation("tovar_out.flg", @"D:\c#\tovar_out.ctlg", NapravFile.TO_ARTIX,new MyMusic()));
            exF.Operations.Add(new Operation("tovar_in.flg", @"D:\c#\tovar_in.ctlg", NapravFile.TO_BUFULL, new MyMusic()));

        }

        public void Run()
        {
            this.fsw.EnableRaisingEvents = true;
            Console.WriteLine("слежка началась!");
        }

        public void fsw_Changed(object sender, FileSystemEventArgs e)
        {

            using (SqlConnection conn = DBUtils.GetDBConnection(DBSQLServerUtils.SERVER))
            {
                try
                {
                    conn.Open();
                    string json;

                    var operation = exF.Operations.Find(x => x.f_flag == e.Name);

                    if (operation != null && e.ChangeType != WatcherChangeTypes.Deleted && e.ChangeType != WatcherChangeTypes.Renamed)
                    {
                        operation.exchange(conn);
                        /*
                        if (operation.napr == NapravFile.TO_BUFULL)
                        {
                           

                            json = FileOperations.readfile(operation.f_exchange);
                            MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(json);  // json decodes
                            SqlConn.DBUtils.executeProcIns(conn, operation., newMusic);
                            }
                        }

                        else if (operation.napr == NapravFile.TO_ARTIX)
                        {
                            MyMusic myCollection = SqlConn.DBUtils.executeProcSelect(conn, operation.sp);
                            string serialized = JsonConvert.SerializeObject(myCollection);   // json code                       
                            FileOperations.writeFile(operation.f_exchange, serialized);

                        }

                     Console.WriteLine("Файл " + e.FullPath + " " + e.ChangeType);
                         * */
                        fsw.EnableRaisingEvents = false; //отключаем слежение
                    }
                }

                catch (Exception ex)
                {
                    FileOperations.saveException(ex);
                }

                finally
                {
                    fsw.EnableRaisingEvents = true; //переподключаем слежение 
                }
            }
        }

    }
}
