namespace com.company.spancy.backend.dto
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MentorName { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, MentorName={MentorName}]";
        }
    }
}