using DataCollectorService.WebApi.Jobs;
using DataCollectorService.WebApi.Services;
using Hangfire;
using Polly;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//builder.Services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "Redis";
});
builder.Services.AddHttpClient<IBinanceService, BinanceService>()
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
    
//builder.Services.AddHangfire(config => config.UseSqlServerStorage(connectionString));
//builder.Services.AddHostedService<PriceUpdateSubscriber>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

//app.UseHangfireDashboard();
//RecurringJob.AddOrUpdate<BinanceDataJob>(
//    "FetchPrices",
//    job => job.FetchPricesJob(),
//    Cron.Hourly);

app.Run();