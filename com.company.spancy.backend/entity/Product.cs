namespace com.company.spancy.backend.entity
{
    public class Product
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}]";
        }
    }
}