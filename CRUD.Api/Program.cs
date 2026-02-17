using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using CRUD.Data;
using CRUD.Data.Contract;
using CRUD.Data.Irepository;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(IGRepository<,>), typeof(GRepository<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CRUD Api",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD Api v1");
       c.RoutePrefix = ""; 
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();



app.Run();


