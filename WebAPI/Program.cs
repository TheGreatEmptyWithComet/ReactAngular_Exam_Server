var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AddControllers adds the necessary services for web API controllers to your application.
builder.Services.AddControllers();

//Adds services required to generate the Swagger / OpenAPI specification document
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
