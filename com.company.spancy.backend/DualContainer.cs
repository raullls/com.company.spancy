using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Log4Net;
using Nancy.TinyIoc;
using Spring.Context;
using Spring.Context.Support;

namespace com.company.spancy.backend
{
    public class DualContainer
    {
        private static readonly Common.Logging.ILog log = LogManager.GetLogger<DualContainer>();
        public TinyIoCContainer TinyIoCContainer { get; set; }
        public IApplicationContext ApplicationContext { get; set; }
        public DualContainer() 
        {
            if (ContextRegistry.IsContextRegistered("spring.root"))
            {
                this.ApplicationContext = ContextRegistry.GetContext("spring.root");
            }
            else
            {
                this.ApplicationContext = new XmlApplicationContext(new string[]
                {
                    "file://config/modules.xml",
                    "file://config/services.xml",
                    "file://config/daos.xml",
                    "file://config/resources.xml",
                    "file://config/validators.xml",
                    "file://config/mappers.xml",
                    "file://config/aspects.xml",
                    "file://config/logging.xml"
                    // "file://config/binders.xml",
                });
                ContextRegistry.RegisterContext(this.ApplicationContext);
            }
            this.TinyIoCContainer = new TinyIoCContainer();
        }
        public DualContainer(IApplicationContext applicationContext, TinyIoCContainer tinyIoCContainer)
        {
            this.ApplicationContext = applicationContext;
            this.TinyIoCContainer = tinyIoCContainer;
        }
    }
}