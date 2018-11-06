using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Shipul
{
    public enum NapravFile
    {
        TO_ARTIX = 1,
        TO_BUFULL = 2
    }



    class ExchangeFiles
    {
        public List<Operation> Operations { get; set; }

        public ExchangeFiles()
        {
            Operations = new List<Operation>();
        }
       
    }

    class Operation
    {
        public string f_flag { get; set; }
        public string f_exchange { get; set; }
        public string sp { get; set; }
        public NapravFile napr { get; set; }
        public ModelExchange model { get; set; }

        public Operation(string f_flag, string f_exchange, NapravFile napr, ModelExchange model)
        {
            this.f_exchange = f_exchange;
            this.f_flag = f_flag;
            this.sp = sp;
            this.napr = napr;
            this.model = model;
        }


        public void exchange(SqlConnection conn){
            string json;
            switch (this.napr) {
                case NapravFile.TO_BUFULL :
                      json = FileOperations.readfile(this.f_exchange);
                     Type type = this.model.GetType();   
                     this.model = JsonConvert.DeserializeObject<type>(json);  // json decodes
                     SqlConn.DBUtils.executeProcIns(conn, this.model.proc.Find(x => x.GetType() == typeof(ProcIns)), this.model);
                    break;

                case NapravFile.TO_ARTIX:
                     json = SqlConn.DBUtils.executeProcSelect(conn, this.model.proc.Find(x => x.GetType() == typeof(ProcSelect)));
                     //string serialized = JsonConvert.SerializeObject(model);   // json code                       
                     FileOperations.writeFile(this.f_exchange, json);

                    break;
            }
        }

    }
}
