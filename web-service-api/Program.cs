using web_service_api;
using web_service_api.Hubs;
using web_service_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IMessagesService,MessageService>();
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<IConnectedUserService, ConnectedUserService>();
builder.Services.AddDbContext<WebServiceContext>(ServiceLifetime.Transient);

builder.Services.AddMvcCore();
builder.Services.AddSignalR();
builder.Services.AddMvc();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow All",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

});



var app = builder.Build();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000") //Source
        .AllowAnyHeader()
        .WithMethods("GET", "POST","DELETE","PUT","PATCH")
        .AllowCredentials();
    builder.WithOrigins("http://localhost:3001") //Source
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "DELETE", "PUT", "PATCH")
    .AllowCredentials();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Allow All");

app.MapHub<MyHub>("/api/hub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

