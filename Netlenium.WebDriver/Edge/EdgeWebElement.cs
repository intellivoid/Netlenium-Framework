﻿// <copyright file="EdgeWebElement.cs" company="Microsoft">
// Licensed to the Software Freedom Conservancy (SFC) under one
// or more contributor license agreements. See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership. The SFC licenses this file
// to you under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using Netlenium.WebDriver.Remote;

namespace Netlenium.WebDriver.Edge
{
    /// <summary>
    /// Provides a mechanism to get elements off the page for test
    /// </summary>
    public class EdgeWebElement : RemoteWebElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeWebElement"/> class
        /// </summary>
        /// <param name="parent">Driver in use</param>
        /// <param name="elementId">Id of the element</param>
        public EdgeWebElement(EdgeDriver parent, string elementId)
            : base(parent, elementId)
        {
        }
    }
}