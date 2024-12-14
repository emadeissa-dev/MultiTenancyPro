using MultiTenancyPro.Services;
using MultiTenancyPro.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITenantServices, TenantServices>();

builder.Services.Configure<TenantSetting>
    (builder.Configuration.GetSection(nameof(TenantSetting)));

TenantSetting options = new();
builder.Configuration.GetSection(nameof(TenantSetting)).Bind(options);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
