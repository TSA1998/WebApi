using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestApi.Models
{
    public class RestContext : DbContext
    {
        public RestContext(DbContextOptions<RestContext> options)
            : base(options)
        {
        }

        public DbSet<DishItem> DishItems { get; set; }
    }
}