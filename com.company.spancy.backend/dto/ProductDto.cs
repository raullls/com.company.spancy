namespace com.company.spancy.backend.dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}]";
        }
    }
}