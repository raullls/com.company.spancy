namespace com.company.spancy.backend.entity
{
    public class Person
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Phone> Phones { get; set; } = new List<Phone>();
    }
}