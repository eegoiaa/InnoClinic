using InnoClinic.Common.MIddleware;
using InnoClinic.Profiles.Application.Configuration;
using InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;
using InnoClinic.Profiles.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.AddMessaging(typeof(GetDoctorsListHandler).Assembly, builder.Configuration);

builder.Services.AddControllers();
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

