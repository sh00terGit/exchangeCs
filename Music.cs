using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shipul;

namespace Shipul.Music
{


    class MyMusic : ModelExchange
    {
        public List<Track> Tracks { get; set; }

        public MyMusic() : base()
        {
            Tracks = new List<Track>();
            proc = new List<StoreProc>() {                
                new ProcSelect("sp_track_sel", null),
                new ProcIns("sp_track_ins", new List<SpParam> {
                    new SpParam("@album","Album"),
                    new SpParam("@artist","Artist"),
                    new SpParam("@title","Title"),
                    new SpParam("@year","Year")
                })
            };
    }

                  
        }
    

    
    class Track 
    {
        public Track()
        {
            this.Album = "";
            this.Artist = "";
            this.Title = "";
            this.Year = 0;
        }

        public Track(string Album,string Artist,string Title, int Year)
        {
            this.Album = Album;
            this.Artist = Artist;
            this.Title = Title;
            this.Year = Year;
        }
    
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

    }

}
