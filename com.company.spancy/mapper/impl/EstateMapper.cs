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
    public class EstateMapper : BaseMapper<EstateDto, Estate>, IBaseMapper<EstateDto, Estate>
    {
        public EstateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BuildingDto, Building>();
                cfg.CreateMap<Building, BuildingDto>();
                cfg.CreateMap<LandDto, Land>();
                cfg.CreateMap<Land, LandDto>();
                cfg.CreateMap<EstateDto, Estate>()
                    .ConvertUsing((src, dst) =>
                    {
                        if (src is BuildingDto)
                        {
                            return this.mapper.Map(src, new Building());
                        }
                        else
                        {
                            return this.mapper.Map(src, new Land());
                        }
                    });
                cfg.CreateMap<Estate, EstateDto>()
                    .ConvertUsing((src, dst) =>
                    {
                        if (src is Building)
                        {
                            BuildingDto buildingDto = this.mapper.Map(src, new BuildingDto());
                            buildingDto.Type = "building";
                            return buildingDto;
                        }
                        else
                        {
                            LandDto landDto = this.mapper.Map(src, new LandDto());
                            landDto.Type = "land";
                            return landDto;
                        }
                    });
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
