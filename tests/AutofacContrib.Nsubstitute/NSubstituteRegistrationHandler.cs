using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using NSubstitute;

namespace AutofacContrib.NSubstitute {
    /// <summary> Resolves unknown interfaces and Mocks using the <see cref="Substitute" />. </summary>
    internal class NSubstituteRegistrationHandler : IRegistrationSource
    {
        private static readonly IReadOnlyCollection<Type> GenericCollectionTypes = new List<Type>
        {
            typeof(IEnumerable<>),
            typeof(IList<>),
            typeof(IReadOnlyCollection<>),
            typeof(ICollection<>),
            typeof(IReadOnlyList<>)
        };

        /// <summary>
        ///     Retrieve a registration for an unregistered service, to be used
        ///     by the container.
        /// </summary>
        /// <param name="service">The service that was requested.</param>
        /// <param name="registrationAccessor"></param>
        /// <returns>
        ///     Registrations for the service.
        /// </returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor
            (Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            var typedService = service as IServiceWithType;

            if (typedService == null ||
                !typedService.ServiceType.GetTypeInfo().IsInterface ||
                IsGenericListOrCollectionInterface(typedService.ServiceType) ||
                typedService.ServiceType.IsArray ||
                typeof(IStartable).IsAssignableFrom(typedService.ServiceType))
                return Enumerable.Empty<IComponentRegistration>();

            var rb = RegistrationBuilder.ForDelegate((c, p) => Substitute.For(new[] { typedService.ServiceType }, null))
                .As(service)
                .InstancePerLifetimeScope();

            return new[] { rb.CreateRegistration() };
        }

        public bool IsAdapterForIndividualComponents
        {
            get { return false; }
        }

        private static bool IsGenericListOrCollectionInterface(Type serviceType)
        {
            // TODO make backwards compatible
            //return serviceType.IsGenericType && GenericCollectionTypes.Contains(serviceType.GetGenericTypeDefinition());
            return serviceType.GetTypeInfo().IsGenericType && GenericCollectionTypes.Contains(serviceType.GetGenericTypeDefinition());
        }
    }
}