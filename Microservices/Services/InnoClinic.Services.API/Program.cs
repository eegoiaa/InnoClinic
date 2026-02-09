using InnoClinic.Services.Application.Services.Queries.GetServices;
using InnoClinic.Services.Infrastructure.Configuration;
using System.Text.Json.Serialization;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Host.UseWolverine(options =>
{
    options.Discovery.IncludeAssembly(typeof(GetServicesQuery).Assembly);
    options.UseRabbitMq(new Uri("amqp://guest:guest@localhost:5672"))
           .AutoProvision()
           .DeclareExchange("specialization-updates", exchange =>
           {
               exchange.ExchangeType = ExchangeType.Fanout;
           })
           .BindExchange("specialization-updates")
           .ToQueue("services-specializations");
    options.ListenToRabbitQueue("services-specializations");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
