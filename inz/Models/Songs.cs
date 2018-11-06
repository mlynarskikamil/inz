using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inz.Models
{
    public class Songs
    {
        public int ID { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }

        internal static Task<string> ToListAsync()
        {
            throw new NotImplementedException();
        }
        //public int ID_Song { get; set; }
        //public int ID_MP3 { get; set; }
        //public int ID_Artist { get; set; }
        //public int ID_Album { get; set; }
        //public int ID_Producent { get; set; }
        //public int ID_Text_Song { get; set; }
        //public DateTime Relase_Date_Song { get; set; }
    }
}
