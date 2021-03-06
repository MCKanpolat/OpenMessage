﻿using AutoFixture;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMessage.Samples.Core.Services
{
    internal sealed class ProducerService<T> : BackgroundService
        where T : new()
    {
        private readonly IDispatcher<T> _dispatcher;
        private readonly Fixture _fixture = new Fixture();

        public ProducerService(IDispatcher<T> dispatcher) => _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                try
                {
                    await Task.Delay(1000);
                    await _dispatcher.DispatchAsync(_fixture.Create<T>());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }
    }
}