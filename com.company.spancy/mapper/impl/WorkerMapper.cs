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
    public class WorkerMapper : BaseMapper<WorkerDto, Worker>, IBaseMapper<WorkerDto, Worker>
    {
        public WorkerMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkerDto, Worker>()
                    .AfterMap((src, dest) =>
                    {
                        dest.WorkerWorkers = this.Dao.LoadEntity(src.Id)?.WorkerWorkers;
                    });
                cfg.CreateMap<Worker, WorkerDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
