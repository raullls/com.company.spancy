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
    public class ItemModule : NancyModule
    {
        public IItemService ItemService { get; set; }
        public ItemModule() : base("/onetomanybidirectional")
        {
            this.Get("/findAll", x =>
            {
                return this.ItemService.FindAllRO();
            });

            this.Get("/findById/{itemid}", x =>
            {
                return this.ItemService.FindByIdRO((long)x.itemid);
            });

            this.Post("/create", x =>
            {
                ItemDto itemDto = this.Bind<ItemDto>();
                Response response = this.ItemService.CreateItemTX(itemDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{itemid}", x =>
            {
                return this.ItemService.RemoveByIdTX((long)x.itemid);
            });

            this.Post("/edit", x =>
            {
                ItemDto itemDto = this.Bind<ItemDto>();
                Response response = this.ItemService.UpdateItemTX(itemDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
