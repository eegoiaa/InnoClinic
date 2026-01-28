using InnoClinic.Profiles.Application.Configuration;
using InnoClinic.Profiles.Application.Doctors.Queries.GetDoctorsList;
using InnoClinic.Profiles.Infrastructure.Configuration;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Host.UseWolverine(options => {
    options.Discovery.IncludeAssembly(typeof(GetDoctorsListHandler).Assembly);
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

