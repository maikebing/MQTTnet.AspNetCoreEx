# MQTTnet.AspNetCoreEx

```c#
services.AddHostedMqttServerEx();


   app.UseMqttServerEx(server =>
            {
                server.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(args => mqttEventsHandler.Server_ClientConnected(server, args));
            //ClientConnectionValidatorHandler 
                server.ClientConnectionValidatorHandler = new MqttServerClientConnectionValidatorHandlerDelegate(args => mqttEventsHandler.Server_ClientConnectionValidator(server, args));
            });

`
```

