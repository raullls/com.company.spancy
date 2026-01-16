using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class WorkerWorkerTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Worker worker1 = new Worker();
            worker1.Name = "Lalit Narayan Mishra";

            this.SessionFactory.GetCurrentSession().Save(worker1);

            Worker worker2 = new Worker();
            worker2.Name = "Amritendu De";

            this.SessionFactory.GetCurrentSession().Save(worker2);

            WorkerWorker workerWorker1 = new WorkerWorker();
            workerWorker1.Worker1 = worker1;
            workerWorker1.Worker2 = worker2;
            workerWorker1.Relationship = "Createspace";

            this.SessionFactory.GetCurrentSession().Save(workerWorker1);

            Worker worker3 = new Worker();
            worker3.Name = "Amish Tripathi";

            this.SessionFactory.GetCurrentSession().Save(worker3);

            Worker worker4 = new Worker();
            worker4.Name = "Amritendu De";

            this.SessionFactory.GetCurrentSession().Save(worker4);

            WorkerWorker workerWorker2 = new WorkerWorker();
            workerWorker2.Worker1 = worker3;
            workerWorker2.Worker2 = worker4;
            workerWorker2.Relationship = "Brother";

            this.SessionFactory.GetCurrentSession().Save(workerWorker2);

            worker3.Name = "Hazekul Alam";
            this.SessionFactory.GetCurrentSession().Save(worker3);
            IList<WorkerWorker> list = this.SessionFactory.GetCurrentSession().CreateQuery("from WorkerWorker").List<WorkerWorker>();

            Assert.AreEqual(2, list.Count());

            IList<WorkerWorker> workerWorkerList = this.SessionFactory.GetCurrentSession().CreateQuery("from WorkerWorker").List<WorkerWorker>();
            WorkerWorker tmpWorkerWorker = workerWorkerList[0];

            this.SessionFactory.GetCurrentSession().Delete(tmpWorkerWorker);

            IList<WorkerWorker> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from WorkerWorker").List<WorkerWorker>();

            Assert.AreEqual(1, list2.Count());
        }
    }
}
