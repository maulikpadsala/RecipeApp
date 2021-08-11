using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAppAPI.Database
{
    public class RecipeDataContext: DbContext
    {
        public RecipeDataContext(DbContextOptions options)
      : base(options)
        { }

        public DbSet<Recipe> recipes { get; set; }
    }
}
