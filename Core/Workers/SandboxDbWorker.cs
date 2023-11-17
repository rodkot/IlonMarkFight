using Core.Config;
using Core.Sandboxes;
using DataLib.Cards;
using DataLib.Desks;
using DataLib.Desks.Interfaces;
using DataLib.SandBoxes;
using DataLib.SandBoxes.Interfaces;
using DataLib.Shuffler.Interfaces;
using DbStorage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Workers;

public class SandboxDbWorker : BackgroundService
{
    private readonly ILogger<SandboxDbWorker> _logger;
    private readonly ExperimentConditionService _service;
    private readonly IShuffleableDesk _cardDeck;
    private readonly CoreConfig _config;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IDeskShuffler _shuffler;
    private readonly ISandBox _sandbox;


    public SandboxDbWorker(ILogger<SandboxDbWorker> logger, ExperimentConditionService service, ISandBox sandbox, IHostApplicationLifetime lifetime, IDeskShuffler shuffler, CoreConfig config)
    {
        _logger = logger;
        _service = service;
        _sandbox = sandbox;
        _lifetime = lifetime;
        _shuffler = shuffler;
        _config = config;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        switch (_config.Request)
        {
            case DbRequest.Generate:
            {
                _logger.LogInformation("start generating experiment conditions");
                return Generate(stoppingToken);
            }
            case DbRequest.UseGenerated:
            {
                _logger.LogInformation("using already generated experiment conditions");
                return UseGenerated(stoppingToken);
            }
            default:
            {
                _logger.LogError("stopping application");
                _lifetime.StopApplication();
                return Task.CompletedTask;
            }
        }
    }

    private Task UseGenerated(CancellationToken stoppingToken)
    {
        var success = 0;
        var completed = 0;
        try
        {
            var decks = _service.GetFirstN(_config.ExperimentCount);
            for (var i = 0; i < decks.Count && !stoppingToken.IsCancellationRequested; i++)
            {
              
            }
            
            // Console.WriteLine($"\nExperiments completed: {completed}");
            // Console.WriteLine($"Success rate: {(double)success / completed}\n");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
        }
        finally
        {
            _lifetime.StopApplication();
        }
        
        return Task.CompletedTask;
    }

    private Task Generate(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}