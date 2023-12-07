namespace OpponentsWebApp;

public class WebStarter
{
    private static IHostBuilder CreateBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup(w => new Startup(args[0]));
        });
    }
    
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine($"Wrong amount of arguments: expected 1, has {args.Length}");
        }
        var app = CreateBuilder(args).Build();
        app.Run();
    } 
}