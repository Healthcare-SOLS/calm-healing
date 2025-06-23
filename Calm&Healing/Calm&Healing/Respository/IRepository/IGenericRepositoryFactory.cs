namespace Calm_Healing.Respository.IRepository
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
    }

}
