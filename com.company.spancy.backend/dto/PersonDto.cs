namespace com.company.spancy.backend.dto
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<string> Numbers { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, Numbers={Numbers}]";
        }
    }
}