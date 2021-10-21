// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace nanoFramework.Aws.IoTCore.Devices //TODO: improve for AWS IoT.
{
    /// <summary>
    /// Connection and event status.
    /// </summary>
    public enum ConnectorStateMessage
    {
        /// <summary>
        /// Connection happened.
        /// </summary>
        Connected,

        /// <summary>
        /// Disconnection happened.
        /// </summary>
        Disconnected,

        /// <summary>
        /// Shadow has been updated.
        /// </summary>
        ShadowUpdated,

        /// <summary>
        /// Error updating the shadows.
        /// </summary>
        ShadowUpdateError,

        /// <summary>
        /// Shadow received.
        /// </summary>
        ShadowReceived,

        /// <summary>
        /// Shadow update received.
        /// </summary>
        ShadowUpdateReceived,

        /// <summary>
        /// Shadow deleted.
        /// </summary>
        ShadowDeleted,

        /// <summary>
        /// IoT Hub Error.
        /// </summary>
        IoTCoreError,

        /// <summary>
        /// IoT Hub Warning.
        /// </summary>
        IoTCoreWarning,

        /// <summary>
        /// IoT Hub Information.
        /// </summary>
        IoTCoreInformation,

        /// <summary>
        /// IoT Hub Highlight Information.
        /// </summary>
        IoTCoreHighlightInformation,

        /// <summary>
        /// Internal SDK error.
        /// </summary>
        InternalError,

        /// <summary>
        /// Message received.
        /// </summary>
        MessageReceived,

        //TODO: map the following exceptions?!

        // /// <summary>
        // /// The request is not valid.
        // /// <summary>
        // InvalidRequest,

        // /// </summary>
        // /// The specified combination of HTTP verb and URI is not supported.
        // /// <summary>
        // MethodNotAllowed,

        // /// </summary>
        // /// The specified resource does not exist.
        // /// <summary>
        // ResourceNotFound,

        // /// </summary>
        // /// The service is temporarily unavailable.
        // /// <summary>
        // ServiceUnavailable,

        // /// </summary>
        // /// The rate exceeds the limit.
        // /// <summary>
        // Throttling,

        // /// </summary>
        // /// You are not authorized to perform this operation.
        // /// <summary>
        // Unauthorized,

        // /// </summary>
        // /// The document encoding is not supported.
        // /// <summary>
        // UnsupportedDocumentEncoding,

    }
}
