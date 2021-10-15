// Copyright (c) .Net Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using nanoFramework.Json;

namespace nanoFramework.Aws.IoTCore.Devices.Shadows
{
    /// <summary>
    /// Shadow Representation.
    /// <cref="https://docs.aws.amazon.com/iot/latest/developerguide/device-shadow-document.html"/>
    /// </summary>
    public class Shadow
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Shadow()
        {
            //Required for Json deserialization.
        }

        /// <summary>
        /// /// Creates an instance of <see cref="Shadow"/>.
        /// </summary>
        /// <param name="shadowJsonString">Shadow as a JSON string</param>
        public Shadow(string shadowJsonString) //TODO: Perhaps included client token / unique Id?!
        {
            Hashtable _shadow = (Hashtable)JsonConvert.DeserializeObject(shadowJsonString, typeof(Hashtable));

            if (_shadow["state"] != null)
            {
                state = new ShadowProperties((Hashtable)_shadow["state"]);
            }
            if (_shadow["metadata"] != null)
            {
                metadata = new ShadowProperties((Hashtable)_shadow["metadata"]);
            }
            if (_shadow["version"] != null)
            {
                version = (int)_shadow["version"];
            }
            if (_shadow["clienttoken"] != null)
            {
                clienttoken = (string)_shadow["clienttoken"]; //this could be null, or a named shadow?!
            }
            if (_shadow["timestamp"] != null)
            {
                timestamp = (int)_shadow["timestamp"];
            }

        }

#pragma warning disable IDE1006 // Naming Styles, disabled due to being Json specific
        /// <summary>
        /// Gets and sets the <see cref="Shadow"/>  state properties.
        /// </summary>
        public ShadowProperties state { get; set; }

        /// <summary>
        /// Gets and sets the <see cref="Shadow"/> metadata properties.
        /// </summary>
        public ShadowProperties metadata { get; set; }

        /// <summary>
        /// Shadow's Version
        /// </summary>
        public int version { get; set; }

        /// <summary>
        /// Shadow's Client Token
        /// </summary>
        public string clienttoken { get; set; }

        /// <summary>
        /// Shadow's Timestamp
        /// </summary>
        /// <remarks>
        /// Unix Timestamp as 32bit signed integer.
        /// </remarks>
        public int timestamp { get; set; }

#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Gets the Shadow as a JSON string.
        /// </summary>
        /// <param name="updateShadow"> Optional: Only returns a partial json string for use with updates. unless specified.</param>
        /// <returns>JSON string</returns>
        public string ToJson(bool updateShadow = true)
        {
            if (updateShadow)
            {
                //TODO: The following is a workaround (and hacky at that)!
                var shadowStringHeader = @"{""state"":{""reported"":" + JsonConvert.SerializeObject((Hashtable)state.reported) + "}"; //TODO: The conversion is required, otherwise there is an error!
                var shadowStringBody = string.Empty;
                if (!string.IsNullOrEmpty(clienttoken)) //not sure about this one!
                {
                    shadowStringBody = $",clientToken:{ clienttoken }";
                }
                var shadowStringFooter = "}";
                return shadowStringHeader + shadowStringBody + shadowStringFooter;
            }
            else
            {
                JsonConvert.SerializeObject(this);
            }
            return @"{""shadow"" : ""Serialization-Error""}"; //technically unreachable?!
        }
    }
}
