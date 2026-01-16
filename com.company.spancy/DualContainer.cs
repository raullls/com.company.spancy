using Nancy.TinyIoc;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy
{
    public class DualContainer
    {
        public TinyIoCContainer TinyIoCContainer { get; set; }
        public IApplicationContext ApplicationContext { get; set; }
        public DualContainer()
        {
            //this.ApplicationContext = new XmlApplicationContext("file://config/modules.xml");
            //ContextRegistry.RegisterContext(this.ApplicationContext);
            this.ApplicationContext = ContextRegistry.GetContext();
            this.TinyIoCContainer = new TinyIoCContainer();
        }
        public DualContainer(IApplicationContext applicationContext, TinyIoCContainer tinyIoCContainer)
        {
            this.TinyIoCContainer = tinyIoCContainer;
            this.ApplicationContext = applicationContext;
        }
    }
}
