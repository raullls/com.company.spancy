using com.company.spancy.dto;
using com.company.spancy.service;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.module
{
    public class ManuscriptAuthorModule : NancyModule
    {
        public IManuscriptAuthorService ManuscriptAuthorService { get; set; }
        public ManuscriptAuthorModule() : base("/manytomanybidirectionalwithjoinattribute/manuscriptauthor")
        {
            this.Get("/findAll", x =>
            {
                return this.ManuscriptAuthorService.FindAllRO();
            });

            this.Post("/create", x =>
            {
                ManuscriptAuthorDto manuscriptAuthorDto = this.Bind<ManuscriptAuthorDto>();
                return this.ManuscriptAuthorService.CreateTX(manuscriptAuthorDto);
            });

            this.Post("/isPresent", x =>
            {
                ManuscriptAuthorDto manuscriptAuthorDto = this.Bind<ManuscriptAuthorDto>();
                return this.ManuscriptAuthorService.IsPresentRO(manuscriptAuthorDto);
            });

            this.Post("/remove", x =>
            {
                ManuscriptAuthorDto manuscriptAuthorDto = this.Bind<ManuscriptAuthorDto>();
                return this.ManuscriptAuthorService.RemoveTX(manuscriptAuthorDto);
            });
        }
    }
}
