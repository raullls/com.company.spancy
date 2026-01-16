using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
{
    public class ManuscriptAuthorDto
    {
        public ManuscriptDto ManuscriptDto { get; set; }
        public AuthorDto AuthorDto { get; set; }
        public string Publisher { get; set; }
    }
}
