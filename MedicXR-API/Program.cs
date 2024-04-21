using MedicXR_API.Libraries;
using MedicXR_API.Services;
using MedicXR_API.Services.Athena;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<HttpLibrary>();
builder.Services.AddSingleton<AthenaEMRService>();
builder.Services.AddSingleton<MedicXRService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
