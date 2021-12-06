using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCoreEx;
using MQTTnet.Diagnostics.Logger;
using System;
using System.Threading.Tasks;

namespace MQTTnet.Server
{
    public interface IMqttServerClientConnectionValidatorHandler
    {
        Task HandleClientConnectionValidatorAsync(MqttServerClientConnectionValidatorEventArgs eventArgs);
    }
}