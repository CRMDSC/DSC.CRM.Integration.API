using DSC.CRM.Integration.API.Helpers;
using DSC.CRM.Integration.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// Register CRM Connector
builder.Services.AddScoped<CRMConnector>();
builder.Services.AddScoped<EstablishmentService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<NocService>();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
