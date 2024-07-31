using Microsoft.EntityFrameworkCore;
using Npgsql;
using Tekton.Repository;
using Tekton.Repository.Interfaces;
using Tekton.Services;
using Tekton.Services.Interface;
using Tekton.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILogService, LogService>();

//DbContext
builder.Services.AddDbContext<Context>(options => 
    options.UseNpgsql(new NpgsqlConnection(builder.Configuration.GetSection("ConnectionString").Get<string>()),
    b => b.MigrationsAssembly("Tekton.Api"))  
);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddLazyCache();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();


