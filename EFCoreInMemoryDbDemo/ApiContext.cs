using brochureapi.Models;
using Microsoft.EntityFrameworkCore;


namespace brochureapi.EFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {
     
       

        public ApiContext(DbContextOptions<ApiContext> options)
           : base(options)
        {
        }
       

        public DbSet<Brochure> Brochures { get; set; }
    }
}
