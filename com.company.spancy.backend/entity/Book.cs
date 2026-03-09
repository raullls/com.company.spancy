namespace com.company.spancy.backend.entity
{
    public class Book
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Shipping Shipping { get; set; }
    }
}