// Copyright (c) .Net Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace nanoFramework.Aws.IoTCore.Devices
{
    /// <summary>
    /// Connection status.
    /// </summary>
    public class ConnectorState
    {
        internal ConnectorState(ConnectorState status)
        {
            State = status.State;
            Message = status.Message;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConnectorState()
        { }

        /// <summary>
        /// The status.
        /// </summary>
        public ConnectorStateMessage State { get; set; }

        /// <summary>
        /// The associated message if any.
        /// </summary>
        public string Message { get; set; }
    }
}