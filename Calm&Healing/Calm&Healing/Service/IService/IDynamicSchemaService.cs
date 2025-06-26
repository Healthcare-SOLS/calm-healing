namespace Calm_Healing.Service.IService
{
    public interface IDynamicSchemaService
    {
        Task CreateSchemaAndTablesAsync(string schemaName);
    }
}
