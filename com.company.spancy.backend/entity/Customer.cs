namespace com.company.spancy.backend.entity
{
    public class Customer
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Cart Cart { get; set; }
    }
}