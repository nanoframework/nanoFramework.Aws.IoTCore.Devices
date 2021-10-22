//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;

namespace nanoFramework.Aws.IoTCore.Devices
{
    /// <summary>
    /// Cloud to device delegate function used for callback.
    /// </summary>
    /// <param name="sender">The <see cref="MqttConnectionClient"/> class sender.</param>
    /// <param name="e">The device message event arguments.</param>
    public delegate void CloudToDeviceMessage(object sender, CloudToDeviceMessageEventArgs e);

    /// <summary>
    /// The device message event arguments.
    /// </summary>
    public class CloudToDeviceMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor for device message event arguments.
        /// </summary>
        /// <param name="message">The string message.</param>
        /// <param name="topic">The topic that the message was received.</param>
        public CloudToDeviceMessageEventArgs(string message, string topic)
        {
            Message = message;
            Topic = topic;
        }
        

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// The Topic.
        /// </summary>
        public string Topic { get; set; }

    }
}

