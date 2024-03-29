﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inz.Models
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
            this.ID_Song = ID_Song;
            this.Title = Title;
            this.Name_Artist = Name_Artist;
            this.Name_Album = Name_Album;
            this.Name_Producer = Name_Producer;
            this.Name_mp3 = Name_mp3;
            this.UrlAzure = UrlAzure;
            this.ReleaseSong = DateTime.Now;
        }
        public int ID_Song { get; set; }

        [Required(ErrorMessage = "Proszę wpisać tytuł")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Proszę wpisać nazwę artysty")]
        [Display(Name = "Artysta")]
        public string Name_Artist { get; set; }

        [Display(Name = "Album")]
        public string Name_Album { get; set; }

        [Display(Name = "Producent")]
        public string Name_Producer { get; set; }

        [Display(Name = "Data publikacji")]
        public DateTime ReleaseSong { get; set; }

        [Display(Name = "Odnośnik do teledysku")]
        public string VideoUrl { get; set; }

        public string TextSong { get; set; }

        public IFormFile Name_mp3 { get; set; }

        public string UrlAzure { get; set; }
    }
}
