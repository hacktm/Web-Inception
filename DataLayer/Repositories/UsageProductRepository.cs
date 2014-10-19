using System.Data.Entity;
using System.Linq;

namespace DataLayer.Repositories
{
    public class UsageProductRepository : AbstractRepository
    {
        public void Create(UsageProduct model)
        {
            Context.UsageProducts.Add(model);
            Context.SaveChanges();
        }

        public void Update(UsageProduct newUsageProduct)
        {
            var oldUsageProduct = Context.UsageProducts.FirstOrDefault(x => x.ID == newUsageProduct.ID);
            if (oldUsageProduct == null) return;
            oldUsageProduct.AspNetUserId = newUsageProduct.AspNetUserId;
            oldUsageProduct.ShootedUserId = newUsageProduct.ShootedUserId;
            Context.Entry(oldUsageProduct).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int usageProductId)
        {
            var usageProduct = Context.UsageProducts.FirstOrDefault(x => x.ID == usageProductId);
            if (usageProduct == null) return;
            Context.UsageProducts.Remove(usageProduct);
            Context.SaveChanges();
        }
    }
}
