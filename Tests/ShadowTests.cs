//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.TestFramework;
using System;

namespace nanoFramework.Aws.IoTCore.Devices.Tests
{
    [TestClass]
    public class ShadowTests
    {
        [TestMethod]
        public void get_shadow_json_to_class()
        {
            var json = ShadowJsonMessageExamples.Get_AcceptedShadow;
        }

        [TestMethod]
        public void get_shadow_class_to_json()
        {
            //technically un-necessary, but a good check!
            // need to make sure the above method passes before attempting this!
        }

        [TestMethod]
        public void update_shadow_recieved_json_to_class()
        {
            var json = ShadowJsonMessageExamples.Update_AcceptedShadow;
        }

        [TestMethod]
        public void create_update_shadow_to_send_class()
        {
            //technically un-necessary, but a good check!
            //in the real world, this would be a different class.
            // need to make sure the above method passes before attempting this!
        }

        [TestMethod]
        public void update_delta_shadow_received_json_to_class()
        {
            //TODO: there is no reference for this (yet).
        }

    }
}


//using System;
//using System.Diagnostics;
//using System.Security.Cryptography.X509Certificates;
//using System.Threading;
//using nanoFramework.Aws.IoTCore.Devices;

//namespace nanoFramework.Aws.Tests
//{
//    public class program //TODO: finish tests!
//    {
//        MqttConnectionClient _awsMqttClient;

//        void Setup() {
//            //TODO: should this use the AWS CLI to provision an instance, or at least reccomend one is already setup???

//            //X509Certificate caCert = new X509Certificate(AwsIoTCoreDefaultRootCA);
//            //X509Certificate2 clientCert = new X509Certificate2(AwsIoTCoreUriInstance.ClientRsaSha256Crt, AwsIoTCoreUriInstance.ClientRsaKey, ""); //make sure to add a correct pfx certificate

//            //_awsMqttClient = new MqttConnectionClient("AwsIoTCoreUriInstance", "nanoFrameworkTestRunner", clientCert, nanoFramework.M2Mqtt.Messages.MqttQoSLevel.AtLeastOnce, caCert);
//        }

//        void Connect_to_the_mqtt_broker() 
//        {
//            //might have to take into account network config, time, and availability here!
//            _awsMqttClient.Open();
//        }

//        void Send_a_telemetry_message_to_the_broker() { }

//        void Receive_a_client_message_from_the_broker() { }

//        void Get_the_unnamed_shadow()
//        {
//            var shadow = _awsMqttClient.GetShadow(new CancellationTokenSource(15000).Token);
//            if (shadow != null)
//            {
//                Debug.WriteLine($"Get shadow result:");
//                //Debug.WriteLine($"Desired:  {shadow.state.desired.ToJson()}");
//                //Debug.WriteLine($"Reported:  {shadow.state.reported.ToJson()}");
//            }
//        }

//        void Update_the_unnamed_shadow() { }

//        void Delete_the_unnamed_shadow() { }

//        void Handle_the_delta_unnamed_shadow() { }

//        void Cleanup() 
//        {
//            _awsMqttClient.Close();
//        }
//    }
//}

