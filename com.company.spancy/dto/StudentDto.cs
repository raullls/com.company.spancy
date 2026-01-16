using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
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
