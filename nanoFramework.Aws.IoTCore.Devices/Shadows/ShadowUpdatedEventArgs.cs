// Copyright (c) .Net Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace nanoFramework.Aws.IoTCore.Devices.Shadows
{
    /// <summary>
    /// Delegate for Shadow updated event.
    /// </summary>
    /// <param name="sender">The <see cref="MqttConnectionClient"/> sender.</param>
    /// <param name="e">The Shadow updated event arguments.</param>
    public delegate void ShadowUpdated(object sender, ShadowUpdateEventArgs e);

    /// <summary>
    /// Shadow updated event arguments.
    /// </summary>
    public class ShadowUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor for Shadow updated event arguments.
        /// </summary>
        /// <param name="shadow">The updated shadow.</param>
        public ShadowUpdateEventArgs(Shadow shadow) //(ShadowPropertyCollection shadow)
        {
            Shadow = shadow;
        }

        /// <summary>
        /// Shadow.
        /// </summary>
        public Shadow Shadow { get; set; }

    }
}
