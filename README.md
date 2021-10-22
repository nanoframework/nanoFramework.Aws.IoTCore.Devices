
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoFramework.Aws.IoTCore.Devices&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoFramework.Aws.IoTCore.Devices)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoFramework.Aws.IoTCore.Devices&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework.Aws.IoTCore.Devices)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/dt/nanoFramework.Aws.IoTCore.Devices.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Aws.IoTCore.Devices/)
[![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md)
[![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)


![nanoFramework logo](https://github.com/nanoframework/Home/blob/main/resources/logo/nanoFramework-repo-logo.png)

-----

# Welcome to the .NET **nanoFramework** AWS IoT Core SDK repository!

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| nanoFramework.Aws.IoTCore.Devices | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Aws.IoTCore.Devices/_apis/build/status/nanoframework.nanoFramework.Aws.IoTCore.Devices?branchName=main)](https://dev.azure.com/nanoframework/nanoframework.Aws.IoTCore.Devices/_build/latest?definitionId=81&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Aws.IoTCore.Devices.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Aws.IoTCore.Devices/) |
| nanoFramework.Aws.IoTCore.Devices (preview) | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Aws.IoTCore.Devices/_apis/build/status/nanoframework.nanoFramework.Aws.IoTCore.Devices?branchName=develop)](https://dev.azure.com/nanoframework/nanoFramework.Aws.IoTCore.Devices/_build/latest?definitionId=81&branchName=develop) | [![NuGet](https://img.shields.io/nuget/vpre/nanoFramework.Aws.IoTCore.Devices.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Aws.IoTCore.Devices/) |

## See it in action

TODO! Please help us to add some samples!

## Usage

**Important**: Due to TLS, You **must** be connected to the internet (which means already having an IP address from your router) **and** a valid (minimum of today) date and time. Please check the examples that use networking (especially those that include TLS/NTP) to help you ensure it will work.

This AWS IoT SDK is using MQTT. So you need to ensure you have an outbound firewall port open to the TCP port 8883. If you are in an enterprise network, this may be blocked.

If it is, in future, we plan to add WebSocket support.

The the name of the classes and the methods try to get close to the [.NET nanoFramework C# Azure IoT SDK](https://github.com/nanoFramework/nanoFramework.Azure.Devices). This should allow an easier portability of the code between both cloud provider environments.

### Certificate

You have 2 options to provide the right Azure IoT TLS certificate:

- Pass it in the constructor
- Store it into the device

The [X.509Certificates](https://docs.aws.amazon.com/iot/latest/developerguide/x509-client-certs.html) contain, for your convenience, the default root certificate used to connect to AWS IoT.

#### Thru the constructor

You will have to embed the certificate into your code, or add it as a resource:

```csharp
const string AwsRootCA = @"-----BEGIN CERTIFICATE-----
MIIDQTCCAimgAwIBAgITBmyfz5m/jAo54vB4ikPmljZbyjANBgkqhkiG9w0BAQsF
ADA5MQswCQYDVQQGEwJVUzEPMA0GA1UEChMGQW1hem9uMRkwFwYDVQQDExBBbWF6
b24gUm9vdCBDQSAxMB4XDTE1MDUyNjAwMDAwMFoXDTM4MDExNzAwMDAwMFowOTEL
MAkGA1UEBhMCVVMxDzANBgNVBAoTBkFtYXpvbjEZMBcGA1UEAxMQQW1hem9uIFJv
b3QgQ0EgMTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALJ4gHHKeNXj
ca9HgFB0fW7Y14h29Jlo91ghYPl0hAEvrAIthtOgQ3pOsqTQNroBvo3bSMgHFzZM
9O6II8c+6zf1tRn4SWiw3te5djgdYZ6k/oI2peVKVuRF4fn9tBb6dNqcmzU5L/qw
IFAGbHrQgLKm+a/sRxmPUDgH3KKHOVj4utWp+UhnMJbulHheb4mjUcAwhmahRWa6
VOujw5H5SNz/0egwLX0tdHA114gk957EWW67c4cX8jJGKLhD+rcdqsq08p8kDi1L
93FcXmn/6pUCyziKrlA4b9v7LWIbxcceVOF34GfID5yHI9Y/QCB/IIDEgEw+OyQm
jgSubJrIqg0CAwEAAaNCMEAwDwYDVR0TAQH/BAUwAwEB/zAOBgNVHQ8BAf8EBAMC
AYYwHQYDVR0OBBYEFIQYzIU07LwMlJQuCFmcx7IQTgoIMA0GCSqGSIb3DQEBCwUA
A4IBAQCY8jdaQZChGsV2USggNiMOruYou6r4lK5IpDB/G/wkjUu0yKGX9rbxenDI
U5PMCCjjmCXPI6T53iHTfIUJrU6adTrCC2qJeHZERxhlbI1Bjjt/msv0tadQ1wUs
N+gDS63pYaACbvXy8MWy7Vu33PqUXHeeE6V/Uq2V8viTO96LXFvKWlJbYK8U90vv
o/ufQJVtMVT8QtPHRh8jrdkPSHCa2XV4cdFyQzR1bldZwgJcJmApzyMZFo6IQ6XU
5MsI+yMRQ+hDKXJioaldXgjUkK642M4UwtBV8ob2xJNDd2ZhwLnoQdeXeGADbkpy
rqXRfboQnoZsG4q5WTP468SQvvG5
-----END CERTIFICATE-----
";
MqttConnectionClient awsIoT = new MqttConnectionClient(IotBrokerAddress, ThingId, clientCert, MqttConnectionClient.QoSLevel.AtLeastOnce, awsRootCACert: new X509Certificate(AzureRootCA));
```

You can place your binary certificate in the resources as well and just get the certificate from it:

```csharp
X509Certificate awsRootCACert = new X509Certificate(Resources.GetBytes(Resources.BinaryResources.AwsCAcertificate));
MqttConnectionClient awsIoT = new MqttConnectionClient(IotBrokerAddress, ThingId, clientCert, MqttConnectionClient.QoSLevel.AtLeastOnce, awsRootCACert);
```

Note: when the certificate expires, you will have to fully reflash the device with the new certificates and keys, or build a mechanism into your code to include primary and secondary versions of them.

#### Storing the certificate into the device

You can store the certificate in the device flash and not in the code, so if you have to change the certificate, you'll just have to clean the current store and upload the new one. Edit the network properties:

![edit device network](readme-assets/device-network.jpg)

Navigate to the `General` tab:

![device network certificate](readme-assets/device-network-certificate.jpg)

Browse to choose your certificate, it can be in a binary (crt, der) or string form (pem, txt) and select ok. The certificate to connect will be selected automatically during the connection.

### Creating an MqttConnectionClient

You can connect to AWS IoT Core using a certificate containing the private certificate and key, or IAM (not currently supported). The following example shows how to create it:

```csharp
const string ThingId = "nanoDeviceShadow";
const string IotBrokerAddress = "<account>-ats.iot.<region>.amazonaws.com";
//make sure to add a correct pfx certificate
X509Certificate2 clientCert = new X509Certificate2(ClientRsaSha256Crt, ClientRsaKey, "");
MqttConnectionClient awseIoT = new MqttConnectionClient(IotBrokerAddress, ThingId, clientCert, MqttConnectionClient.QoSLevel.AtLeastOnce, awsRootCACert);

```

Note: please see the previous section to understand how to better pass the certificate for your usage. The example shows the certificate uploaded into the device and not in the code.

### Getting and updating Device Shadow

You can request your Shadow simply by calling the `GetShadow` function.

```csharp
var shadow = awsIoT.GetShadow(new CancellationTokenSource(20000).Token);
if (shadow == null)
{
    Debug.WriteLine($"Can't get the shadow");
    awsIoT.Close();
    return;
}

Debug.WriteLine($"Shadow ClientToken: {shadow.DeviceId}, #desired: {shadow.status.desired}, #reported: {shadow.status.reported}");
```

Note: it's important to use a `CancellationToken` that be cancelled after a certain amount of time. Otherwise, this will be blocking the thread up to the point the shadow will be received. 

Shadows have status properties, reported and desired. They are a hashtable and you can get or try to get any element.

You can report your Shadow as simple as this:


TODO: does not work yet!!!
```csharp
ShadowCollection reported = new ShadowCollection();
reported.Add("firmware", "myNano");
reported.Add("sdk", 0.2);
awsIoT.UpdateReportedProperties(reported);
```

You also have the option to wait for the shadow update confirmation, in this case use a `CancellationToken` that can be cancelled. Otherwise the check will be ignored.

Note: the function will return false if the shadow reception confirmation is not checked or if it did not arrive on time.

You can also register for any shadow update:

```csharp
awsIoT.ShadowUpated += ShadowUpdatedEvent;

void ShadowUpdatedEvent(object sender, ShadowUpdateEventArgs e)
{
    Debug.WriteLine($"Shadow update received:  {e.Shadow}");
}
```

### Sending message

You have to use the `SendMessage` function to send any kind of message or telemetry to AWS IoT. As with the other function, you have the possibility to ensure delivery using a `CancellationToken` than can be cancelled. If one that can't be cancelled is used, the delivery insurance will be ignored and the function will return false.

```csharp
var isReceived = awsIoT.SendMessage($"{{\"Temperature\":42,\"Pressure\":1024}}", new CancellationTokenSource(5000).Token);
Debug.WriteLine($"Message received by IoT Core: {isReceived}");
```

Note: The message will be send with the same quality of service you created the connection with. You won't get any answer for the quality 0. In this case, you can simplify it to:

```csharp
awsIoT.SendMessage($"{{\"Temperature\":42,\"Pressure\":1024}}");
```

### Cloud to device messages

You can register an event to receive Cloud to device messages:


TODO: check works!!!

```csharp
awsIoT.CloudToDeviceMessage += CloudToDeviceMessageEvent;

// The following example shows how to display all keys in debug
void CloudToDeviceMessageEvent(object sender, CloudToDeviceMessageEventArgs e)
{
    Debug.WriteLine($"Message arrived: {e.Message}");
    foreach (string key in e.Properties.Keys)
    {
        Debug.Write($"  Key: {key} = ");
        if (e.Properties[key] == null)
        {
            Debug.WriteLine("null");
        }
        else
        {
            Debug.WriteLine((string)e.Properties[key]);
        }
    }

    // e.Message contains the message itself
    if(e.Message == "stop")
    {
        ShoudIStop = true;
    }
}
```

Note: the `sender` is a `MqttConnectionClient` class, you can then send a message back with a confirmation or any logic you've put in place.


### Status update event

A status update event is available:

```csharp
awsIoT.StatusUpdated += StatusUpdatedEvent;

void StatusUpdatedEvent(object sender, StatusUpdatedEventArgs e)
{
    Debug.WriteLine($"Status changed: {e.Status.State}, {e.Status.State}");
    // You may want to reconnect or use a similar retry mechanism
    ////if (e.Status.State == Status.Disconnected)
    ////{
    ////    mqtt.Open();
    ////}
}
```

Note that they are status change based, so once the connect or disconnect event arrives, they'll be replaced by other events as soon as something else happened like receiving a shadow.


## Future work items for discussion:

* Better documentation about ensuring "persistent" connections (or not) with documentation (including cloud policy doc for support)
* Add some integration tests, including (scripts to auto provision cloud broker (and/or)) manual setup documents to ensure ease of use.
* Partial Greengrass support?!
* Websocket support?!
* fleet provisioning support?!


## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The .NET **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
