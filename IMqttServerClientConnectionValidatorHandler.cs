using MQTTnet.AspNetCoreEx;
using System.Threading.Tasks;

namespace MQTTnet.Server
{
    public interface IMqttServerClientConnectionValidatorHandler
    {
        Task HandleClientConnectionValidatorAsync(MqttServerClientConnectionValidatorEventArgs eventArgs);
    }
}