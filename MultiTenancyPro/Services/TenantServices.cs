using Microsoft.Extensions.Options;
using MultiTenancyPro.Settings;

namespace MultiTenancyPro.Services;

public class TenantServices : ITenantServices
{
    private readonly TenantSetting _tenantSetting;
    private HttpContext? httpcontext;
    private Tenant? currentTenant;
    public TenantServices(HttpContextAccessor httpContextAccessor, IOptions<TenantSetting> tenantSetting)
    {
        _tenantSetting = tenantSetting.Value;
        httpcontext = httpContextAccessor.HttpContext;
        if (httpcontext != null)
        {
            if (httpcontext.Request.Headers.TryGetValue("tenant", out var tenantId))
            {
                currentTenant = _tenantSetting.Tenants.FirstOrDefault(x => x.TenantId == tenantId);
                if (currentTenant == null)
                {
                    throw new Exception("invalid tenant Id");
                }
                if (string.IsNullOrEmpty(currentTenant.ConnectionString))
                {
                    currentTenant.ConnectionString = _tenantSetting.Defaults.ConnectionString;
                }
            }
        }


    }
    public string? GetConnectionString()
    {
        var currentConnectionString = currentTenant is null
            ? _tenantSetting.Defaults.ConnectionString
            : currentTenant.ConnectionString;

        return currentConnectionString;
    }

    public Tenant? GetCurrentTenant()
    {
        return currentTenant;
    }

    public string? GetDataBaseProvider()
    {
        return _tenantSetting.Defaults.DBProvider;
    }
}
