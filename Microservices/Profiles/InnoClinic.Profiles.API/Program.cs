using Common.Events;
using InnoClinic.Profiles.Application.Configuration;
using InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;
using InnoClinic.Profiles.Infrastructure.Configuration;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Host.UseWolverine(options => {
    options.Discovery.IncludeAssembly(typeof(GetDoctorsListHandler).Assembly);
    options.UseRabbitMq(new Uri("amqp://guest:guest@localhost:5672")).AutoProvision();
    options.PublishMessage<SpecializationCreatedEvent>()
           .ToRabbitExchange("specialization-updates", exchange =>
           {
               exchange.ExchangeType = ExchangeType.Fanout;
           });
});

builder.Services.AddControllers();
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

