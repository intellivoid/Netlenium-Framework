﻿// <copyright file="How.cs" company="WebDriver Committers">
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

namespace Netlenium.Driver.WebDriver.Support.PageObjects
{
    /// <summary>
    /// Provides the lookup methods for the FindsBy attribute (for using in PageObjects)
    /// </summary>
    public enum How
    {
        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.Id" />
        /// </summary>
        Id,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.Name" />
        /// </summary>
        Name,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.TagName" />
        /// </summary>
        TagName,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.ClassName" />
        /// </summary>
        ClassName,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.CssSelector" />
        /// </summary>
        CssSelector,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.LinkText" />
        /// </summary>
        LinkText,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.PartialLinkText" />
        /// </summary>
        PartialLinkText,

        /// <summary>
        /// Finds by <see cref="Netlenium.Driver.WebDriver.By.XPath" />
        /// </summary>
        XPath,

        /// <summary>
        /// Finds by a custom <see cref="By"/> implementation.
        /// </summary>
        Custom
    }
}