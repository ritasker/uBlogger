using System;
using System.Collections.Generic;
using MediatR;
using Nancy.Testing;

namespace uBlogger.Tests.Unit.Helpers
{
    public static class BootstrapperExtensions
    {
        public static void Mediatr(this ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator @this, Dictionary<Type, object> singleInstances = null, Dictionary<Type, object[]> multipleInstances = null)
        {
            var mediator = new Mediator(t => SingleInstanceFactory(t, singleInstances), t => MultiInstanceFactory(t, multipleInstances));
            @this.Dependency<IMediator>(mediator);
        }

        private static IEnumerable<object> MultiInstanceFactory(Type serviceType, Dictionary<Type, object[]> instances)
        {
            return instances[serviceType];
        }

        private static object SingleInstanceFactory(Type serviceType, Dictionary<Type, object> instances)
        {
            return instances[serviceType];
        }
    }
}