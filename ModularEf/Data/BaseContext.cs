using Inventory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ModularEf.Data
{
    public class BaseContext :  IdentityDbContext<IdentityUser>
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseInventory();
        }
    }
}