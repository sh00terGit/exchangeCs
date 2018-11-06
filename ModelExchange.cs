using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipul
{
    class ModelExchange
    {
        public List<StoreProc> proc { get; set; }
        public object obj { get; set; }

        public ModelExchange(List<StoreProc> proc)
        {
            this.proc = proc;
        }

        public ModelExchange()
        {
            proc = new List<StoreProc>();
        }

    }
    
    class StoreProc
    {
        public string sp_name {get;set;}
        public List<SpParam> prms { get; set; }

        public StoreProc(string sp_name, List<SpParam> prms)
        {
            this.sp_name = sp_name;
            this.prms = prms;
        }
    }

    class ProcIns : StoreProc
    {
        public ProcIns(string sp_name, List<SpParam> prms)
            : base(sp_name,prms)
        {
        }
    }

    class ProcSelect : StoreProc
    {
        public ProcSelect(string sp_name, List<SpParam> prms)
            : base(sp_name,prms)
        {
        }
    }

    class SpParam {
        public string param_name { get; set; }
        public string prop_name { get; set; }

        public SpParam(string param_name,string prop_name) 
        {
            this.prop_name = prop_name;
            this.param_name = param_name;
        }

        
    }


}
