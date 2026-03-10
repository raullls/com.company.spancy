namespace com.company.spancy.backend.entity
{
    public class Cart
    {
        public virtual long Id { get; set; }
        public virtual double Amount { get; set; }
        public virtual Customer Customer { get; set; }
    }
}