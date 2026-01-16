using com.company.spancy.dao;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using NHibernate;
using Spring.Testing.Microsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test
{
    [TestClass]
    public class BaseTest : AbstractTransactionalSpringContextTests
    {
        protected Browser Browser { get; set; }
        private SpringNancyBootstrapper SpringNancyBootstrapper { get; set; }
        public ISessionFactory SessionFactory { get; set; }
        public bool DisableInterceptor { get; set; }

        public BaseTest(bool disableInterceptor = false)
        {
            this.DisableInterceptor = disableInterceptor;
        }

        [TestInitialize]
        public void DoTestInitialize()
        {
            this.SpringNancyBootstrapper = new SpringNancyBootstrapper();
            this.Browser = new Browser(this.SpringNancyBootstrapper);
        }

        [TestCleanup]
        public void DoTestCleanup()
        {
            base.TestCleanup();
        }
        protected override string[] ConfigLocations
        {
            get
            {
                return new string[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/modules.xml"),
                    this.DisableInterceptor ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/aspects.test.xml") : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/aspects.xml"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/resources.xml"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/validators.xml"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/services.xml"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/daos.xml"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/binders.xml"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config/mappers.xml")
                };
            }
        }
    }
}
