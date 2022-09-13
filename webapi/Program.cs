using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

// builder.Services.AddSingleton(session_store);
builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    //加入Newtonsoft.Json.Serialization;這行 才能讓 前端傳過來的 JSON 都自動序列化
    option.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
