namespace SYTD_4_Examples.ServiceLocator.Complex
{
    public class ComplexServiceLocator : IServiceProvider
    {
        private record ServiceNode(Type Implementation, Type[] Dependencies);

        private readonly Dictionary<Type, ServiceNode> nodeMap = new();
        private readonly Dictionary<Type, object> instanceMap = new();

        public ComplexServiceLocator()
        {
            // add self as service too
            instanceMap.Add(typeof(IServiceProvider), this);
        }

        public void Add<TInterface, TImplementation>()
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);

            var dependencies = GetDependencies(implementationType).ToArray();
            var node = new ServiceNode(implementationType, dependencies);

            nodeMap.Add(interfaceType, node);
        }

        private IEnumerable<Type> GetDependencies(Type type)
        {
            // assumes each service has a single constructor
            var constructor = type.GetConstructors().Single();
            return constructor.GetParameters().Select(p => p.ParameterType);
        }

        public object GetService(Type interfaceType)
        {
            if (!instanceMap.TryGetValue(interfaceType, out var instance))
            {
                if (!nodeMap.TryGetValue(interfaceType, out var node))
                    throw new Exception($"No service found for type {interfaceType.Name}");

                var parameters = node.Dependencies.Select(GetService).ToArray();
                instance = Activator.CreateInstance(node.Implementation, parameters);
                instanceMap.Add(interfaceType, instance);
            }

            return instance;
        }

        public T? GetService<T>() where T : class
        {
            return GetService(typeof(T)) as T;
        }
    }
}