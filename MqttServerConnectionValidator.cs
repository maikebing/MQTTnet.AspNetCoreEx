using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQTTnet.AspNetCoreEx
{
    internal class MqttServerConnectionValidator : IMqttServerConnectionValidator
    {
        internal MqttServerClientConnectionValidatorHandlerDelegate Handler { get; set; }
          
        public Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            Handler.HandleClientConnectionValidatorAsync(new MqttServerClientConnectionValidatorEventArgs() { Context = context });
            return Task.CompletedTask;
        }
    }
}
