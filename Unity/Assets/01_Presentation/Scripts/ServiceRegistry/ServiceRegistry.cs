using System.Collections.Generic;

namespace Game.Presentation
{
    public class ServiceRegistry
    {
        private List<IService> services = new List<IService>();

        public void Register(IService service)
        {
            services.Add(service);
        }

        public void Unregister(IService service)
        {
            services.Remove(service);
        }

        public T Get<T>()
            where T : IService
        {
            for (int i = 0; i < services.Count; i++)
            {
                if (typeof(T).IsAssignableFrom(services[i].GetType()))
                {
                    return (T)services[i];
                }
            }

            throw new System.Exception($"The requested service of type \"{typeof(T)}\" was not found.");
        }
    }
}
