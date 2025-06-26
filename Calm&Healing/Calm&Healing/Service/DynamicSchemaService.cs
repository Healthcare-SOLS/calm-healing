using Calm_Healing.DAL.DynamicModels;
using Calm_Healing.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Calm_Healing.Service
{
    public class DynamicSchemaService : IDynamicSchemaService
    {
        private readonly Func<string, DynamicTenantDbContext> _contextFactory;

        public DynamicSchemaService(Func<string, DynamicTenantDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateSchemaAndTablesAsync(string schemaName)
        {
            try
            {
                // STEP 1: Create schema
                using var tempDb = _contextFactory("public");
                await tempDb.Database.ExecuteSqlRawAsync($"CREATE SCHEMA IF NOT EXISTS \"{schemaName}\"");

                // STEP 2: Check if tables exist in the new schema
                using var db = _contextFactory(schemaName);
                var conn = db.Database.GetDbConnection();
                await conn.OpenAsync();
                int tableCount;
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"
                SELECT COUNT(*) 
                FROM information_schema.tables 
                WHERE table_schema = '{schemaName}' AND table_type = 'BASE TABLE'";
                    tableCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                if (tableCount == 0)
                {
                    // Generate script from a context configured for public schema
                    using var publicDb = _contextFactory(schemaName);
                    var createScript = publicDb.Database.GenerateCreateScript();

                    // Replace public schema with dynamic schema
                    var dynamicScript = createScript
                        .Replace("CREATE TABLE public.", $"CREATE TABLE \"{schemaName}\".")
                        .Replace("CREATE INDEX \"IX_", $"CREATE INDEX \"{schemaName}_IX_")
                        .Replace(" ON public.", $" ON \"{schemaName}\".")
                        .Replace("REFERENCES public.", $"REFERENCES \"{schemaName}\".")
                        .Replace("CREATE TABLE", "CREATE TABLE IF NOT EXISTS");
                    var patien = publicDb.Patients.ToList();
                    await db.Database.ExecuteSqlRawAsync(dynamicScript);
                }

                Console.WriteLine($"✅ Schema '{schemaName}' ready with all tables!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                throw;
            }
        }

    }
}
    


