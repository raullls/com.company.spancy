namespace com.company.spancy.backend.dto
{
    public class BookDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, City={City}]";
        }
    }
}