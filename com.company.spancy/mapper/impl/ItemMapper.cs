using AutoMapper;
using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.mapper.impl
{
    public class ItemMapper : BaseMapper<ItemDto,Item>, IBaseMapper<ItemDto, Item>
    {
        public ItemMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemDto, Item>()
                    .ForMember(dest => dest.Features, opt => opt.MapFrom((src, dest, ctx) =>
                    {
                        IList<Feature> features = new List<Feature>();
                        if (src.FeatureList != null)
                        {
                            Item item = this.Dao.FindByValueObject(this.Hql, new Item { Id = src.Id, Name = src.Name }).SingleOrDefault() ?? new Item() { Features = new List<Feature>() };
                            foreach (string feature in src.FeatureList)
                            {
                                features.Add(item.Features.Where(x => x.Name.Equals(feature)).SingleOrDefault() ?? new Feature { Name = feature, Item = dest });
                            }
                            this.Dao.Evict(item);
                        }
                        return features;
                    }));

                cfg.CreateMap<Item, ItemDto>()
                    .ForMember(dest => dest.FeatureList, opt => opt.MapFrom(src => src.Features.Select(x => x.Name).ToList()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
