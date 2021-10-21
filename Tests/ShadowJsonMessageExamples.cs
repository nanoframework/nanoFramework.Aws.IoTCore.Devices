// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace nanoFramework.Aws.IoTCore.Devices.Tests
{
    /// <summary>
    /// Connection status.
    /// </summary>
    public static class ShadowJsonMessageExamples
    {
        public static string Get_AcceptedShadow = @"
{""state"" : {
    ""desired"" : {
      ""welcome"" : ""aws-iot""
    },
    ""reported"" : {
      ""welcome"" : ""aws-iot"",
      ""cpu"" : ""STM32F7"",
      ""operatingSystem"" : ""nanoFramework"",
      ""bootTimestamp"" : ""2021-08-31T16:11:39.813Z"",
      ""serialNumber"" : ""310042001751373137393238"",
      ""platform"" : ""ORGPAL_PALTHREE""
    }
  },
  ""metadata"" : {
    ""desired"" : {
        ""welcome"" : {
            ""timestamp"" : 1616429277
        }
    },
    ""reported"" : {
    ""welcome"" : {
        ""timestamp"" : 1616429277
        },
      ""cpu"" : {
        ""timestamp"" : 1630426335
      },
      ""operatingSystem"" : {
        ""timestamp"" : 1630426335
      },
      ""bootTimestamp"" : {
        ""timestamp"" : 1630426335
      },
      ""serialNumber"" : {
        ""timestamp"" : 1630426335
      },
      ""platform"" : {
        ""timestamp"" : 1630426335
      }
}
},
  ""version"" : 1538,
  ""timestamp"" : 1630428596
}";

        public static string Update_AcceptedShadow_Document = @"
{
  ""previous"" : {
    ""state"" : {
      ""desired"" : {
        ""welcome"" : ""aws-iot""
      },
      ""reported"" : {
        ""welcome"" : ""aws-iot"",
        ""cpu"" : ""STM32F7"",
        ""operatingSystem"" : ""nanoFramework"",
        ""bootTimestamp"" : ""2021-10-04T23:09:38.299Z"",
        ""serialNumber"" : ""SN310042001751373137393238"",
        ""platform"" : ""ORGPAL_PALTHREE""
      }
    },
    ""metadata"" : {
      ""desired"" : {
        ""welcome"" : {
          ""timestamp"" : 1616429277
        }
      },
      ""reported"" : {
        ""welcome"" : {
          ""timestamp"" : 1616429277
        },
        ""cpu"" : {
          ""timestamp"" : 1633388981
        },
        ""operatingSystem"" : {
          ""timestamp"" : 1633388981
        },
        ""bootTimestamp"" : {
          ""timestamp"" : 1633388981
        },
        ""serialNumber"" : {
          ""timestamp"" : 1633388981
        },
        ""platform"" : {
          ""timestamp"" : 1633388981
        }
      }
    },
    ""version"" : 1753
  },
  ""current"" : {
    ""state"" : {
      ""desired"" : {
        ""welcome"" : ""aws-iot""
      },
      ""reported"" : {
    ""welcome"" : ""aws-iot"",
        ""cpu"" : ""STM32F7"",
        ""operatingSystem"" : ""nanoFramework"",
        ""bootTimestamp"" : ""2021-10-04T23:11:08.217Z"",
        ""serialNumber"" : ""SN310042001751373137393238"",
        ""platform"" : ""ORGPAL_PALTHREE""
      }
    },
    ""metadata"" : {
    ""desired"" : {
        ""welcome"" : {
            ""timestamp"" : 1616429277
        }
    },
      ""reported"" : {
        ""welcome"" : {
            ""timestamp"" : 1616429277
        },
        ""cpu"" : {
            ""timestamp"" : 1633389076
        },
        ""operatingSystem"" : {
            ""timestamp"" : 1633389076
        },
        ""bootTimestamp"" : {
            ""timestamp"" : 1633389076
        },
        ""serialNumber"" : {
            ""timestamp"" : 1633389076
        },
        ""platform"" : {
            ""timestamp"" : 1633389076
        }
    }
},
    ""version"" : 1754
  },
  ""timestamp"" : 1633389076
}
";

        public static string Update_AcceptedShadow = @"
{""state"" : {
    ""reported"" : {
      ""cpu"" : ""STM32F7"",
      ""operatingSystem"" : ""nanoFramework"",
      ""bootTimestamp"" : ""2021-08-31T16:11:39.813Z"",
      ""serialNumber"" : ""310042001751373137393238"",
      ""platform"" : ""ORGPAL_PALTHREE""
    }
},
  ""metadata"" : {
    ""reported"" : {
        ""cpu"" : {
            ""timestamp"" : 1630426335
        },
      ""operatingSystem"" : {
            ""timestamp"" : 1630426335
      },
      ""bootTimestamp"" : {
            ""timestamp"" : 1630426335
      },
      ""serialNumber"" : {
            ""timestamp"" : 1630426335
      },
      ""platform"" : {
            ""timestamp"" : 1630426335
      }
    }
},
  ""version"" : 1538,
  ""timestamp"" : 1630426335
}";
    }
}
