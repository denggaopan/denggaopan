using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Denggaopan.WorkerServiceDemo
{
    public class WorkerPro : BackgroundService
    {
        private readonly ILogger<WorkerPro> _logger;

        public WorkerPro(ILogger<WorkerPro> logger)
        {
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WorkerPro starting at: {time}", DateTimeOffset.Now);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("WorkerPro running tasks at: {time}", DateTimeOffset.Now);

                var task1 = Run1(stoppingToken);
                var task2 = Run2(stoppingToken);
                var task3 = Run3(stoppingToken);

                await Task.WhenAll(task1, task2, task3);
            }
            catch (Exception)
            {
                _logger.LogInformation("WorkerPro running exception at: {time}", DateTimeOffset.Now);
            }
            finally
            {
                _logger.LogInformation("WorkerPro running finally at: {time}", DateTimeOffset.Now);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WorkerPro stoping at: {time}", DateTimeOffset.Now);
            await base.StopAsync(cancellationToken);
        }


        protected Task Run1(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("WorkerPro Run1 running at: {time}", DateTimeOffset.Now);
                    Thread.Sleep(1000);
                }
            },stoppingToken);
            return task;
        }

        protected Task Run2(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("WorkerPro Run2 running at: {time}", DateTimeOffset.Now);
                    Thread.Sleep(2000);
                }
            }, stoppingToken);
            return task;
        }

        protected Task Run3(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("WorkerPro Run3 running at: {time}", DateTimeOffset.Now);
                    Thread.Sleep(3000);
                }
            }, stoppingToken);
            return task;
        }

    }
}
