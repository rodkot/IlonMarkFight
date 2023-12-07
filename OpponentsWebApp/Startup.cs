using Core.Opponents;
using Core.Strategies;
using DataLib.Opponents;

namespace OpponentsWebApp;

public class Startup
{
    private readonly Opponent _opponent;

    public Startup(string playerName)
    {
        _opponent = playerName switch
        {
            "Elon" => new Ilon(new PickFirstCardStrategy()),
            "Mark" => new Mark(new PickFirstCardStrategy()),
            _ => throw new ArgumentException("bad player name")
        };
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton(_opponent);
        Console.WriteLine($"{_opponent.Name}: services configured");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionMiddleware();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoint => { endpoint.MapControllers(); });

        Console.WriteLine($"{_opponent.Name}: configured");
    }
}