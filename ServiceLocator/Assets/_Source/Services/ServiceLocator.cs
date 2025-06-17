using System;
using System.Collections.Generic;

namespace Services
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void RegisterService<T>(T service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            Type serviceType = typeof(T);

            _services[serviceType] = service;
        }

        public bool GetService<T>(out T service)
        {
            Type serviceType = typeof(T);

            service = default;
            return false;
        }

        public T GetService<T>()
        {
            if (GetService<T>(out var service))
            {
                return service;
            }

            throw new InvalidOperationException($"Service of type {typeof(T)} is not registered");
        }

        public bool HasService<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        public bool UnregisterService<T>()
        {
            return _services.Remove(typeof(T));
        }
    }
}