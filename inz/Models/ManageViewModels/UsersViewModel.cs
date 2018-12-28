using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inz.Models.ManageViewModels
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRole { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
