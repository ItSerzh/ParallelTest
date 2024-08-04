
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParallelTest;
using static System.Formats.Asn1.AsnWriter;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IOutput, ConsoleOutput>();
builder.Services.AddSingleton<ArrayGenerator>();
builder.Services.AddSingleton<ParallelMeasure>();

var host = builder.Build();
var scope = host.Services.CreateScope();

var parallelMeasure = scope.ServiceProvider.GetRequiredService<ParallelMeasure>();
parallelMeasure.Measure();
