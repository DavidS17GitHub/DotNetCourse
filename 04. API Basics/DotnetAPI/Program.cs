var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// We need to allow CORS so other aplications comming from different origins
// than the ones set in the launchSettings.json can access our application, like for example
// frontend clients
builder.Services.AddCors((options) => 
    {
        options.AddPolicy("DevCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("http://localhost:4200, http://localhost:3000, http://localhost:8000")
                    /* 4200 (Angular),  3000 (React), 8000 (Vue) */
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        options.AddPolicy("ProdCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("https://myProductionSite.com")
                    /* The domains that will be accessing our API */
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors"); /* Here we make use of the CORS policies we created earlier */
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseCors("ProdCors"); /* Here we make use of the CORS policies we created earlier */
    app.UseHttpsRedirection();
}

app.MapControllers();



app.Run();

