using Microsoft.Extensions.Hosting;
using MQTTnet.Adapter;
using MQTTnet.Diagnostics;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCoreEx
{
    public interface IMqttServerEx : IMqttServer
    {
        MqttServerClientConnectionValidatorHandlerDelegate ClientConnectionValidatorHandler { get; set; }
    }

    public class MqttServerEx : MqttServer, IMqttServerEx
    {
        public MqttServerEx(IEnumerable<IMqttServerAdapter> adapters, IMqttNetChildLogger logger) : base(adapters, logger)
        {
        }

        internal MqttServerConnectionValidator ConnectionValidator { get; set; } = new MqttServerConnectionValidator();

        private MqttServerClientConnectionValidatorHandlerDelegate _clientConnectionValidatorHandler;

        public MqttServerClientConnectionValidatorHandlerDelegate ClientConnectionValidatorHandler
        {
            get
            {
                return _clientConnectionValidatorHandler;
            }
            set
            {
                _clientConnectionValidatorHandler = value;
                ConnectionValidator.Handler = _clientConnectionValidatorHandler;
            }
        }
    }

    public class MqttHostedServerEx : MqttServerEx, IHostedService
    {
        public MqttHostedServerEx(IMqttServerOptions options, IEnumerable<IMqttServerAdapter> adapters, IMqttNetLogger logger)
            : base(adapters, logger.CreateChildLogger(nameof(MqttHostedServerEx)))
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public new IMqttServerOptions Options { get; set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return StartAsync(Options);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return StopAsync();
        }
    }
}