using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inz.Models.Search
{
    public class SearchContext : DbContext
    { 

        public DbSet<SearchViewModel> Search { get; set; }

    }
}
