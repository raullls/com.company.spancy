using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.Extensions;
using Nancy.TinyIoc;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy
{
    public class SpringNancyBootstrapper : NancyBootstrapperWithRequestContainerBase<DualContainer>
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("/", @"wwwroot")
            );
        }

        public override INancyEnvironment GetEnvironment()
        {
            return this.ApplicationContainer.TinyIoCContainer.Resolve<INancyEnvironment>();
        }

        protected override DualContainer CreateRequestContainer(NancyContext context)
        {
            return new DualContainer(this.ApplicationContainer.ApplicationContext, this.ApplicationContainer.TinyIoCContainer.GetChildContainer());
        }

        protected override IEnumerable<INancyModule> GetAllModules(DualContainer container)
        {
            return container.TinyIoCContainer.ResolveAll<INancyModule>(false);
        }

        protected override DualContainer GetApplicationContainer()
        {
            return new DualContainer();
        }

        protected override IEnumerable<IApplicationStartup> GetApplicationStartupTasks()
        {
            return this.ApplicationContainer.TinyIoCContainer.ResolveAll<IApplicationStartup>(false);
        }

        protected override IDiagnostics GetDiagnostics()
        {
            return this.ApplicationContainer.TinyIoCContainer.Resolve<IDiagnostics>();
        }

        protected override INancyEngine GetEngineInternal()
        {
            return this.ApplicationContainer.TinyIoCContainer.Resolve<INancyEngine>();
        }

        protected override INancyEnvironmentConfigurator GetEnvironmentConfigurator()
        {
            return this.ApplicationContainer.TinyIoCContainer.Resolve<INancyEnvironmentConfigurator>();
        }

        protected override INancyModule GetModule(DualContainer container, Type moduleType)
        {
            return (INancyModule)container.ApplicationContext.GetObject(moduleType.Name, moduleType);
        }

        protected override IEnumerable<IRegistrations> GetRegistrationTasks()
        {
            return this.ApplicationContainer.TinyIoCContainer.ResolveAll<IRegistrations>(false);
        }

        protected override IEnumerable<IRequestStartup> RegisterAndGetRequestStartupTasks(DualContainer container, Type[] requestStartupTypes)
        {
            container.TinyIoCContainer.RegisterMultiple(typeof(IRequestStartup), requestStartupTypes);
            return container.TinyIoCContainer.ResolveAll<IRequestStartup>(false);
        }

        protected override void RegisterBootstrapperTypes(DualContainer applicationContainer)
        {
            applicationContainer.TinyIoCContainer.Register<INancyModuleCatalog>(this);
        }

        protected override void RegisterCollectionTypes(DualContainer container, IEnumerable<CollectionTypeRegistration> collectionTypeRegistrations)
        {
            foreach (var collectionTypeRegistration in collectionTypeRegistrations)
            {
                switch (collectionTypeRegistration.Lifetime)
                {
                    case Lifetime.Transient:
                        container.TinyIoCContainer.RegisterMultiple(collectionTypeRegistration.RegistrationType, collectionTypeRegistration.ImplementationTypes).AsMultiInstance();
                        break;
                    case Lifetime.Singleton:
                        container.TinyIoCContainer.RegisterMultiple(collectionTypeRegistration.RegistrationType, collectionTypeRegistration.ImplementationTypes).AsSingleton();
                        break;
                    case Lifetime.PerRequest:
                        throw new InvalidOperationException("Unable to directly register a per request lifetime.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override void RegisterInstances(DualContainer container, IEnumerable<InstanceRegistration> instanceRegistrations)
        {
            foreach (var instanceRegistration in instanceRegistrations)
            {
                container.TinyIoCContainer.Register(instanceRegistration.RegistrationType, instanceRegistration.Implementation);
            }
        }

        protected override void RegisterNancyEnvironment(DualContainer container, INancyEnvironment environment)
        {
            container.TinyIoCContainer.Register(environment);
        }

        protected override void RegisterRequestContainerModules(DualContainer container, IEnumerable<ModuleRegistration> moduleRegistrationTypes)
        {
            foreach (var moduleRegistrationType in moduleRegistrationTypes)
            {
                container.TinyIoCContainer.Register(typeof(INancyModule), moduleRegistrationType.ModuleType, moduleRegistrationType.ModuleType.FullName).AsSingleton();
                if (!container.ApplicationContext.ContainsObjectDefinition(moduleRegistrationType.ModuleType.Name))
                {
                    (container.ApplicationContext as IConfigurableApplicationContext).ObjectFactory.RegisterSingleton(moduleRegistrationType.ModuleType.Name, container.TinyIoCContainer.Resolve(moduleRegistrationType.ModuleType));
                }
            }
        }

        protected override void RegisterTypes(DualContainer container, IEnumerable<TypeRegistration> typeRegistrations)
        {
            foreach (var typeRegistration in typeRegistrations)
            {
                switch (typeRegistration.Lifetime)
                {
                    case Lifetime.Transient:
                        container.TinyIoCContainer.Register(typeRegistration.RegistrationType, typeRegistration.ImplementationType).AsMultiInstance();
                        break;
                    case Lifetime.Singleton:
                        container.TinyIoCContainer.Register(typeRegistration.RegistrationType, typeRegistration.ImplementationType).AsSingleton();
                        break;
                    case Lifetime.PerRequest:
                        throw new InvalidOperationException("Unable to directly register a per request lifetime.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
