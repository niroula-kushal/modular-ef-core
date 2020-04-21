using Microsoft.EntityFrameworkCore;

namespace Inventory
{
    public static class EntityRegisterer
    {
        public static void UseInventory(this ModelBuilder builder)
        {
            builder.Entity<Item>().ToTable("tbl_item");
        }
    }
}