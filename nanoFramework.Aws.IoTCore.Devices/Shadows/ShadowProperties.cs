//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System.Collections;

namespace nanoFramework.Aws.IoTCore.Devices.Shadows
{
    /// <summary>
    /// Represents <see cref="Shadow"/> properties
    /// </summary>
    /// <remarks>
    /// Decodes State and Metadata types.
    /// </remarks>
    public class ShadowProperties
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ShadowProperties"/>
        /// </summary>
        public ShadowProperties()
        {
            //Required for Json deserialization.
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ShadowProperties"/>
        /// </summary>
        /// <param name="shadowProperties">Hashtable for the shadow state properties</param>
        /// <remarks>
        /// Decodes State or Metadata properties.
        /// </remarks>
        public ShadowProperties(Hashtable shadowProperties) //or should this be a property collection?
        {
            if (shadowProperties["desired"] != null)
            {
                desired = (Hashtable)shadowProperties["desired"];
            }

            if (shadowProperties["reported"] != null)
            {
                reported = (Hashtable)shadowProperties["reported"];
            }
        }

#pragma warning disable IDE1006 // Naming Styles, disabled due to being Json specific

        /// <summary>
        /// Gets and sets the <see cref="Shadow"/> desired properties.
        /// </summary>
        public Hashtable desired { get; set; } = new Hashtable();

        /// <summary>
        /// Gets and sets the <see cref="Shadow"/> reported properties.
        /// </summary>
        public Hashtable reported { get; set; } = new Hashtable();

#pragma warning restore IDE1006 // Naming Styles

    }
}
