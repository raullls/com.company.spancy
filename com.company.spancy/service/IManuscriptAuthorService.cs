using com.company.spancy.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IManuscriptAuthorService : IBaseService<ManuscriptAuthorDto>
    {
        object RemoveTX(ManuscriptAuthorDto manuscriptAuthorDto);
        object IsPresentRO(ManuscriptAuthorDto manuscriptAuthorDto);
    }
}
