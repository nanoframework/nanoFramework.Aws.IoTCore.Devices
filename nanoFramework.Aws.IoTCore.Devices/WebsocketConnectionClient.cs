//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Net.WebSockets;
using nanoFramework.Aws.SignatureVersion4;

namespace nanoFramework.Aws.IoTCore.Devices
{
    /// <summary>
    /// AWS IoT Core MQTT over Websocket Connection Client for .NET nanoFramework
    /// </summary>
    public class WebsocketConnectionClient : IDisposable
    {

        public Uri Host { get; set; }
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        const int _wssPort = 443; //Default WSS port.

        WebSocket _webSocket = null;

        /// <summary>
        /// Creates a new MQTT over WebSocket Connection Client
        /// </summary>
        /// <remarks>
        /// Supports Signature Version 4 and Custom authentication over port 443
        /// </remarks>
        public WebsocketConnectionClient()
        {
            // TODO: implement! look to the following:
            // https://github.com/dotnet/MQTTnet/blob/master/Source/MQTTnet.Extensions.WebSocket4Net/WebSocket4NetMqttChannel.cs

            //wss://iot-endpoint/mqtt

            // sign with Signature Version 4
            var signer = new SignerForQueryParameterAuth
            {
                EndpointUri = Host,
                HttpMethod = "GET",
                Service = "iotdevicegateway",
                Region = Region
            };

            // use mqtt client WithWebSocketServer

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Dispose()
        {

        }
    }
}
