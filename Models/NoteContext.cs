using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace noteApi.Models
{
    public class noteContext : DbContext
    {
        public noteContext(DbContextOptions<noteContext> options)
            : base(options)
        {
        }
         public DbSet<noteItem> noteItems { get; set; }
    }
}
