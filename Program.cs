using MongoDb.BLL.Extensions;
using MongoDb.Common;
using MongoDb.DAL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbOptionsSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
//IServiceCollection üzerinden Extensions metotlarýn register kaydý
builder.Services.RegisterRepository().RegisterService();



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
