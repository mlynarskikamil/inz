using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inz.Models
{
    public class CreateViewModel
    {
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

        public IFormFile Name_mp3 { get; set; }
    }
}
