
namespace WebApi_net8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //Можем обратиться к swagger.json по URL localhost:____/swagger/v1/swagger.json
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

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/string", () => { return "Test1"; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Это тестовый api",
                    Description = "Возвращается string \"Test1\""
                }
                );

            //Вернем объект кастомного класса. .WithOpenApi - позволяет добавлять доп информацию (документацию)
            var d = new DayModel();
            app.MapGet("/class", () => { return d.GetCurrentDay(); })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Текущий день недели",
                    Description = "Возвращается в виде строки текущий день недели используя класс DateTime и свойств .Today.DayOfWeek"
                }
                );

            app.MapGet("/number", () => { return 100; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Просто число 100",
                    Description = "Возвращается число 100"
                }
                );
            app.MapGet("/date", () => { return DateTime.Now; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Текущая дата",
                    Description = "Возвращается в виде строки текущая дата"
                }
                );
            app.MapGet("/obj", () => { return new { День = "Пятница", Луна = "Полнолуние", Дата = DateTime.UtcNow }; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Объект с фиксированными свойствами",
                    Description = "Возвращается в виде json объект анонимного типа"
                }
                );



            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            });
            //.WithName("GetWeatherForecast")
            //.WithOpenApi();

            app.Run();
        }
    }
}
