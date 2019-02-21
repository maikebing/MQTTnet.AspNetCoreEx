using Microsoft.Extensions.Hosting;
using MQTTnet.Adapter;
using MQTTnet.Diagnostics;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCoreEx
{
    public interface IMqttServerEx:IMqttServer
    {
        event EventHandler<MqttClientConnectionValidatorEventArgs> ClientConnectionValidator;
    }
    public class MqttServerEx : MqttServer, IMqttServerEx
    {
        public MqttServerEx(IEnumerable<IMqttServerAdapter> adapters, IMqttNetChildLogger logger) : base(adapters, logger)
        {
         
        }

        public void ConnectionValidator(MqttConnectionValidatorContext obj)
        {
            ClientConnectionValidator?.Invoke(this,new MqttClientConnectionValidatorEventArgs() { Context = obj });
        }
        public event EventHandler<MqttClientConnectionValidatorEventArgs> ClientConnectionValidator;
       
    }
    public class MqttHostedServerEx : MqttServerEx, IHostedService
    {
        private   IMqttServerOptions _options;

        public MqttHostedServerEx(IMqttServerOptions options, IEnumerable<IMqttServerAdapter> adapters, IMqttNetLogger logger)
            : base(adapters, logger.CreateChildLogger(nameof(MqttHostedServerEx)))
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        
        public new IMqttServerOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            return StartAsync(_options);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return StopAsync();
        }

   
    }
}
