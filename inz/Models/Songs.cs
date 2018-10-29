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
    }
}
