using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Domain.Models.Entities;
using Hackaton2025.Infrastructure;
using Hackaton2025.Infrastructure.Abstractions;
using Hackaton2025.Infrastructure.Services;
using Hackaton2025.Presentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaginator, Paginator>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<IUniversityFacultyService, UniversityFacultyService>();
builder.Services.AddScoped<IPastProcedureService, PastProcedureService>();
builder.Services.AddScoped<IPastProcedureJuryService, PastProcedureJuryService>();
builder.Services.AddScoped<IPastProcedureJuryMemberService, PastProcedureJuryMemberService>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}


app.Run();
