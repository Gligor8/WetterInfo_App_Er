using Microsoft.EntityFrameworkCore;
using System;
using WetterInfo.Models;

namespace WetterInfo.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ResultView> Responses { get; set; }
       
    }
}
