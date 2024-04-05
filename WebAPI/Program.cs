using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// AddControllers adds the necessary services for web API controllers to your application.
builder.Services.AddControllers();

//Adds services required to generate the Swagger / OpenAPI specification document
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataReader, DataReader>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Adds Swagger UI middleware for exploring your web API endpoints
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

// MapControllers configures the web API controller actions in your app as endpoints.
app.MapControllers();

app.Run();
