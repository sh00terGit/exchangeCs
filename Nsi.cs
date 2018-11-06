using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shipul.Nsi
{
    class Nsi
    {
        /*
            class MyMusic
    {
        public List<Track> Tracks { get; set; }

        public MyMusic()
        {
            Tracks = new List<Track>();
        }
    }
         */
        public class Tovar
        {
            public Invent invent { get; set; }
            public string command { get; set; }

        }


        public class Quantityoptions
        {
            public bool enableQuantityLimit { get; set; }
            public double quantityLimit { get; set; }
            public bool requireQuantityManual { get; set; }
            public string enabledefaultquantity { get; set; }
            public string enablequantityscales { get; set; }
            public string enablequantitybarcode { get; set; }
            public string enablequantitymanual { get; set; }
            public string requirequantitybarcode { get; set; }
            public string requirequantityscales { get; set; }
        }

        public class Priceoptions
        {
            public int requireselectprice { get; set; }
            public int requirepricemanual { get; set; }
            public int enablepricemanual { get; set; }
            public int requiredeferredprice { get; set; }
        }

        public class Inventitemoptions
        {
            public int enabledepartmentmanual { get; set; }
            public int requiredepartmentmanual { get; set; }
            public int disablebackinsale { get; set; }
            public string disableinventback { get; set; }
            public string disableinventshow { get; set; }
            public string enablebarcodescanner { get; set; }
            public string enablebarcodemanual { get; set; }
            public int visualverify { get; set; }
            public int ageverify { get; set; }
            public int requiresalerestrict { get; set; }
        }

        public class Options
        {
            public Quantityoptions quantityoptions { get; set; }
            public Priceoptions priceoptions { get; set; }
            public Inventitemoptions inventitemoptions { get; set; }
        }

        public class Invent
        {
            public string inventcode { get; set; }
            public int deptcode { get; set; }
            public double price { get; set; }
            public double remain { get; set; }
            public DateTime remaindate { get; set; }
            public string discautoscheme { get; set; }
            public string articul { get; set; }
            public int age { get; set; }
            public int alcoholpercent { get; set; }
            public string inventgroup { get; set; }
            public int taxgroupcode { get; set; }
            public int measurecode { get; set; }
            public string name { get; set; }
            public double minprice { get; set; }
            public int alctypecode { get; set; }
            public int paymentobject { get; set; }
            public Options options { get; set; }
        }

        

    }
}
