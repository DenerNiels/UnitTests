namespace Store.Store.Domain.Entities
{
    public class Discount
    {
        public Discount(decimal amount, DateTime expireDate)
        {
            Amount = amount;
            ExpireDate = expireDate;
        }

        public decimal Amount { get; set; }
        public DateTime ExpireDate{ get; set; }

        public bool IsValid()
        {
            return DateTime.Compare(DateTime.Now, ExpireDate) < 0;
        }

        public decimal Value()
        {
            if(IsValid())
            {
                return Amount;
            }
            else
                return 0;
        }
    }
}
