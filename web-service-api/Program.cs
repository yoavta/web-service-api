using web_service_api;
using web_service_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<WebServiceContext>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IMessagesService,MessageService>();
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<IConnectedUserService, ConnectedUserService>();
builder.Services.AddScoped<WebServiceContext>();
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