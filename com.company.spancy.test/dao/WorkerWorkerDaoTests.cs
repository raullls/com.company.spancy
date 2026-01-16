using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class WorkerWorkerDaoTests : BaseTest
    {
        public IWorkerDao WorkerDao { get; set; }
        public IWorkerWorkerDao WorkerWorkerDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.WorkerWorkerDao.GetAll().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            bool status = false;

            Worker worker1 = new Worker();
            worker1.Name = "Lalit Narayan Mishra";
            this.WorkerDao.SaveEntity(worker1);

            Worker worker2 = new Worker();
            worker2.Name = "Amritendu De";
            this.WorkerDao.SaveEntity(worker2);

            WorkerWorker workerWorker = new WorkerWorker();
            workerWorker.Worker1 = worker1;
            workerWorker.Worker2 = worker2;
            workerWorker.Relationship = "Colleagues";

            worker1.WorkerWorkers.Add(workerWorker);
            this.WorkerDao.SaveEntity(worker1);

            Worker worker3 = this.WorkerDao.LoadAllEntities()[0];
            Worker worker4 = this.WorkerDao.LoadAllEntities()[1];

            IList<WorkerWorker> workerWorkerList = this.WorkerWorkerDao.IsPresent(worker3.Id, worker4.Id);
            if (null != workerWorkerList)
            {
                if (workerWorkerList.Count > 0)
                {
                    status = true;
                }
            }
            Assert.IsTrue(status);
        }
    }
}
