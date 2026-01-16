using com.company.spancy.dao;
using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class ManuscriptAuthorService : IManuscriptAuthorService
    {
        public IManuscriptAuthorDao ManuscriptAuthorDao { get; set; }
        public IManuscriptDao ManuscriptDao { get; set; }
        public IAuthorDao AuthorDao { get; set; }
        public IBaseMapper<ManuscriptDto, Manuscript> ManuscriptMapper { get; set; }
        public IBaseMapper<AuthorDto, Author> AuthorMapper { get; set; }

        public object CreateTX(ManuscriptAuthorDto dto)
        {
            Manuscript manuscript = this.ManuscriptDao.LoadEntity(dto.ManuscriptDto.Id);
            Author author = this.AuthorDao.LoadEntity(dto.AuthorDto.Id);

            ManuscriptAuthor manuscriptAuthor = new ManuscriptAuthor();
            manuscriptAuthor.Manuscript = manuscript;
            manuscriptAuthor.Author = author;
            manuscriptAuthor.Publisher = dto.Publisher;

            if (manuscript.ManuscriptAuthors == null)
            {
                manuscript.ManuscriptAuthors = new List<ManuscriptAuthor>() { manuscriptAuthor };
            } 
            else
            {
                manuscript.ManuscriptAuthors.Add(manuscriptAuthor);
            }
            if (author.ManuscriptAuthors == null)
            {
                author.ManuscriptAuthors = new List<ManuscriptAuthor>() { manuscriptAuthor };
            } 
            else
            {
                author.ManuscriptAuthors.Add(manuscriptAuthor);
            }

            return this.ManuscriptDao.SaveEntity(manuscript);
        }

        public IList<ManuscriptAuthorDto> FindAllRO()
        {
            IList<ManuscriptAuthorDto> manuscriptAuthorDtos = new List<ManuscriptAuthorDto>();
            IList<ManuscriptAuthor> manuscriptList = this.ManuscriptAuthorDao.LoadAllEntities();
            foreach (ManuscriptAuthor manuscriptAuthor in manuscriptList)
            {
                ManuscriptAuthorDto manuscriptAuthorDto = new ManuscriptAuthorDto();
                manuscriptAuthorDto.ManuscriptDto = this.ManuscriptMapper.Map(manuscriptAuthor.Manuscript);
                manuscriptAuthorDto.AuthorDto = this.AuthorMapper.Map(manuscriptAuthor.Author);
                manuscriptAuthorDto.Publisher = manuscriptAuthor.Publisher;
                manuscriptAuthorDtos.Add(manuscriptAuthorDto);
            }
            return manuscriptAuthorDtos;
        }

        public ManuscriptAuthorDto FindByIdRO(long id)
        {
            throw new NotImplementedException();
        }

        public object IsPresentRO(ManuscriptAuthorDto manuscriptAuthorDto)
        {
            IList<ManuscriptAuthor> manuscriptAuthorList = this.ManuscriptAuthorDao.IsPresent(manuscriptAuthorDto.ManuscriptDto.Id, manuscriptAuthorDto.AuthorDto.Id);
            return (null != manuscriptAuthorList && manuscriptAuthorList.Count > 0);
        }

        public object RemoveByIdTX(long id)
        {
            throw new NotImplementedException();
        }

        public object RemoveTX(ManuscriptAuthorDto manuscriptAuthorDto)
        {
            Manuscript manuscript = this.ManuscriptDao.LoadEntity(manuscriptAuthorDto.ManuscriptDto.Id);
            Author author = this.AuthorDao.LoadEntity(manuscriptAuthorDto.AuthorDto.Id);

            IList<ManuscriptAuthor> manuscriptAuthorList = this.ManuscriptAuthorDao.IsPresent(manuscriptAuthorDto.ManuscriptDto.Id, manuscriptAuthorDto.AuthorDto.Id);
            foreach (ManuscriptAuthor manuscriptAuthor in manuscriptAuthorList)
            {
                manuscript.ManuscriptAuthors.Remove(manuscriptAuthor);
                author.ManuscriptAuthors.Remove(manuscriptAuthor);
                this.ManuscriptAuthorDao.DeleteEntity(manuscriptAuthor);
            }
            this.AuthorDao.UpdateEntity(author);
            this.ManuscriptDao.UpdateEntity(manuscript);
            return 0;
        }

        public object UpdateTX(ManuscriptAuthorDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
