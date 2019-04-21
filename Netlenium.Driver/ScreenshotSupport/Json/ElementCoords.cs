﻿using System;

namespace Netlenium.Driver.ScreenshotSupport.Json
{
    [Serializable]
    public class ElementCoords
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}