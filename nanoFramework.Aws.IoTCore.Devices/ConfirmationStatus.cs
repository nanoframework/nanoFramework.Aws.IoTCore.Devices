// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace nanoFramework.Aws.IoTCore.Devices
{
    internal class ConfirmationStatus
    {
        public ConfirmationStatus(ushort responseId)
        {
            ResponseId = responseId;
            Received = false;
        }

        public ushort ResponseId { get; set; }
        public bool Received { get; set; }

    }
}
