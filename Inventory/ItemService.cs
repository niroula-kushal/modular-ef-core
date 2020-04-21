using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Inventory
{
    public class ItemService
    {
        private DbContext context;

        public ItemService(DbContext context)
        {
            this.context = context;
        }

        public async Task Create()
        {
            var item = new Item {Name = "New item"};
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }
    }
}