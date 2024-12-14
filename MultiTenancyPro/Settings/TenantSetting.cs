namespace MultiTenancyPro.Settings;

public class TenantSetting
{
    public Confirgration Defaults { get; set; } = default!;
    public List<Tenant> Tenant { get; set; } = new();
}
