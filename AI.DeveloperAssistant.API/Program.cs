using AI.DeveloperAssistant.Application.Interfaces; 
using AI.DeveloperAssistant.Application.Services;
using AI.DeveloperAssistant.Infrastructure.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle\

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHttpClient<IAIProvider, OpenAIProvider>();
builder.Services.AddHttpClient<IAIProvider, GeminiProvider>();
builder.Services.AddScoped<AIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

