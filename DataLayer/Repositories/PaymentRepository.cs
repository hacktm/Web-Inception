using System.Data.Entity;
using System.Linq;

namespace DataLayer.Repositories
{
    public class PaymentRepository : AbstractRepository
    {
        public void Create(Payment model)
        {
            Context.Payments.Add(model);
            Context.SaveChanges();
        }

        public void Update(Payment newPayment)
        {
            var oldPayment = Context.Payments.FirstOrDefault(x => x.ID == newPayment.ID);
            if(oldPayment == null) return;
            oldPayment.AspNetUserId = newPayment.AspNetUserId;
            oldPayment.ExpirationDate = newPayment.ExpirationDate;
            oldPayment.PaymentDate = newPayment.PaymentDate;
            oldPayment.PaymentId = newPayment.PaymentId;
            oldPayment.ProductName = newPayment.ProductName;
            oldPayment.Quantity = newPayment.Quantity;
            oldPayment.Usages = newPayment.Usages;

            Context.Entry(oldPayment).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int paymentId)
        {
            var oldPayment = Context.Payments.FirstOrDefault(x => x.ID == paymentId);
            Context.Payments.Remove(oldPayment);
            Context.SaveChanges();
        }

        public int FindPaymentCount(string aspNetUserId, string productName)
        {
            var count = Context.Payments.Where(x => x.AspNetUserId == aspNetUserId && x.ProductName == productName).ToList().AsEnumerable().Sum(x => x.Quantity);
            return count ?? 0;
        }
    }
}
