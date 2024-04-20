using MedicXR_API.Libraries;
using MedicXR_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<MedicXRContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString(Constants.Database))
//);

builder.Services.AddSingleton<HttpLibrary>();
builder.Services.AddSingleton<MedicXRService>();
builder.Services.AddSingleton<AthenaEMRService>();

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
