using com.company.spancy.entity;
using Spring.Data.NHibernate.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao.impl
{
    public class WorkerWorkerDao : HibernateDaoSupport, IWorkerWorkerDao
    {
        public IList<WorkerWorker> GetAll()
        {
            string hql = "select distinct ww from WorkerWorker ww";
            return HibernateTemplate.Find(hql).Cast<WorkerWorker>().ToList();
        }

        public IList<WorkerWorker> IsPresent(long worker1Id, long worker2Id)
        {
            string hql = "select distinct ww from WorkerWorker ww where ww.Worker1.Id = ? and ww.Worker2.Id = ?";
            return HibernateTemplate.Find(hql, new object[] { worker1Id, worker2Id }).Cast<WorkerWorker>().ToList();
        }
    }
}
