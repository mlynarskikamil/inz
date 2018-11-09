using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inz.Models
{
    public class Song
    {
        [Key]
        public int ID_Song { get; set; }
        public string Title { get; set; }

        public int? ID_Artist { get; set; }
        public virtual Artist Artist { get; set; }

        public int? ID_Album { get; set; }
        public virtual Album Album { get; set; }

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

    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Album { get; set; }
        public string Name_Album { get; set; }

        public ICollection<Song> Songs { get; set; }
    }

    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Artist { get; set; }
        public string Name_Artist { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
