using MedicXR_API.Context;
using MedicXR_API.GlobalConstants;
using MedicXR_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MedicXRContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(Constants.Database))
);

builder.Services.AddScoped<MedicXRService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
