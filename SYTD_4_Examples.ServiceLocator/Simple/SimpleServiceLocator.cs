namespace SYTD_4_Examples.ServiceLocator.Simple
{
    public class SimpleServiceLocator : IServiceProvider
    {
        private readonly Dictionary<Type, Type[]> dependencyMap = new();
        private readonly Dictionary<Type, object> instanceMap = new();

        public void Add<T>()
        {
            var serviceType = typeof(T);
            var dependencies = GetDependencies(serviceType).ToArray();
            dependencyMap.Add(serviceType, dependencies);
        }

        private IEnumerable<Type> GetDependencies(Type type)
        {
            var constructor = type.GetConstructors().Single();
            return constructor.GetParameters().Select(p => p.ParameterType);
        }

        public object? GetService(Type type)
        {
            if (!instanceMap.TryGetValue(type, out var instance))
            {
                var dependencies = GetDependencies(type).ToArray();
                instance = Activator.CreateInstance(type, dependencies);
                instanceMap.Add(type, instance);
            }

            return instance;
        }
    }
}