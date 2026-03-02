using InnoClinic.Appointments.Application.Appointments.Commands.CreateAppointmentCommand;
using InnoClinic.Appointments.Application.Configuration;
using InnoClinic.Appointments.Infrastructure.Configuration;
using InnoClinic.Common.Converters;
using InnoClinic.Common.MIddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.AddMesseging(typeof(CreateAppointment).Assembly, builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;//in order to take the value 00:00 and not 00:00:00
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();