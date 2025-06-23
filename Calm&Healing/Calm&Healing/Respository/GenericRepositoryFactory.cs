using Calm_Healing.Respository.IRepository;

namespace Calm_Healing.Respository
{
    public class GenericRepositoryFactory : IGenericRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
        }
    }

}
