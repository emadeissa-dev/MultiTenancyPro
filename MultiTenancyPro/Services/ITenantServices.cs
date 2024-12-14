using MultiTenancyPro.Settings;

namespace MultiTenancyPro.Services;

public interface ITenantServices
{
    string? GetDataBaseProvider();
    string? GetConnectionString();
    Tenant? GetCurrentTenant();
}
