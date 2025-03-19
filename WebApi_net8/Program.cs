
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
            //����� ���������� � swagger.json �� URL localhost:____/swagger/v1/swagger.json
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
                    Summary = "��� �������� api",
                    Description = "������������ string \"Test1\""
                }
                );

            //������ ������ ���������� ������. .WithOpenApi - ��������� ��������� ��� ���������� (������������)
            var d = new DayModel();
            app.MapGet("/class", () => { return d.GetCurrentDay(); })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "������� ���� ������",
                    Description = "������������ � ���� ������ ������� ���� ������ ��������� ����� DateTime � ������� .Today.DayOfWeek"
                }
                );

            app.MapGet("/number", () => { return 100; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "������ ����� 100",
                    Description = "������������ ����� 100"
                }
                );
            app.MapGet("/date", () => { return DateTime.Now; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "������� ����",
                    Description = "������������ � ���� ������ ������� ����"
                }
                );
            app.MapGet("/obj", () => { return new { ���� = "�������", ���� = "����������", ���� = DateTime.UtcNow }; })
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "������ � �������������� ����������",
                    Description = "������������ � ���� json ������ ���������� ����"
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
