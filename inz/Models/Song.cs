using Microsoft.AspNetCore.Http;
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
        public Song()
        {
            this.ID_Song = ID_Song;
            this.Title = Title;
            this.UrlAzure = UrlAzure;
            this.ID_Artist = ID_Artist;
            this.ID_Album = ID_Album;
            this.ID_Producer = ID_Producer;
        }

        [Key]
        public int ID_Song { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        public string UrlAzure { get; set; }

        public int Like { get; set; }

        public int Unlike { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReleaseSong { get; set; }

        public string VideoUrl { get; set; }

        public string TextSong { get; set; }

        public int? ID_Artist { get; set; }
        public virtual Artist Artist { get; set; }

        public int? ID_Album { get; set; }
        public virtual Album Album { get; set; }

        public int? ID_Producer { get; set; }
        public virtual Producer Producer { get; set; }

        public virtual Opinion Opinion { get; set; }

        internal static Task<string> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Album { get; set; }

        [Display(Name = "Album")]
        public string Name_Album { get; set; }

        public string ImgAlbumUrl { get; set; }

        public string InfoAlbum { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        public ICollection<Song> Songs { get; set; }
    }

    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Artist { get; set; }

        [Display(Name = "Artysta")]
        public string Name_Artist { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        public string ImgArtistUrl { get; set; }

        public ICollection<Song> Songs { get; set; }
    }

    public class Producer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Producer { get; set; }

        [Display(Name = "Producent")]
        public string Name_Producer { get; set; }

        public ICollection<Song> Songs { get; set; }
    }

    public class Opinion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Opinion { get; set; }

        public int? ID_Song { get; set; }
        public virtual Song Song { get; set; }

        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool Direction { get; set; }
    }
}
