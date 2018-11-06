using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Shipul
{
    class FileOperations
    {
        public const string FILE_ERROR = @"d:\c#\error.txt";
        public const string FILE_LOG = @"d:\c#\log.txt";

        public static void writeFile(string filePath, string json)
        {
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Create))
                {
                    StreamWriter writer = new StreamWriter(file, Encoding.UTF8); //создаем «потоковый писатель» и связываем его с файловым потоком
                    writer.Write(json); //записываем в файл
                    FileOperations.logEventToFile(filePath, FileMode.Open);
                    writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
                }
            }
            catch (IOException e)
            {
                FileOperations.saveException(e);
            }
        }

       public static string readfile(string fullName)
        {
            string json = "";
            try
            {

                using (FileStream file = new FileStream(fullName, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(file,Encoding.UTF8); // создаем «потоковый читатель» и связываем его с файловым потоком   
                    FileOperations.logEventToFile(fullName, FileMode.Open);
                    json = reader.ReadToEnd();
                    reader.Close(); //закрываем поток
                }
            }
            catch (IOException e) {
                FileOperations.saveException(e);
            }
            

            return json;
        }

       public static void saveException(Exception e)
       {
           using (FileStream file = new FileStream(FILE_ERROR, FileMode.Append))
           {
               StreamWriter writer = new StreamWriter(file);
               writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " +e.Message); //записываем в файл
               writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
           }
           
       }

       public static void logEventToFile(string path,FileMode mode)
       {
           using (FileStream file = new FileStream(FILE_LOG, FileMode.Append))
           {
               StreamWriter writer = new StreamWriter(file);
               writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + path + " " + mode); //записываем в файл
               writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется 
           }

       }
    }

}
