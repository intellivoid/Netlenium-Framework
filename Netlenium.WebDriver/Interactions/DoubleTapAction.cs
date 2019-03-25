﻿// <copyright file="DoubleTapAction.cs" company="WebDriver Committers">
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

using System;
using Netlenium.WebDriver.Interactions.Internal;

namespace Netlenium.WebDriver.Interactions
{
    /// <summary>
    /// Creates a double tap gesture on a touch screen.
    /// </summary>
    internal class DoubleTapAction : TouchAction, IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleTapAction"/> class.
        /// </summary>
        /// <param name="touchScreen">The <see cref="ITouchScreen"/> with which the action will be performed.</param>
        /// <param name="actionTarget">An <see cref="ILocatable"/> describing an element at which to perform the action.</param>
        public DoubleTapAction(ITouchScreen touchScreen, ILocatable actionTarget)
            : base(touchScreen, actionTarget)
        {
            if (actionTarget == null)
            {
                throw new ArgumentException("Must provide a location for a single tap action.", "actionTarget");
            }
        }

        /// <summary>
        /// Performs the action.
        /// </summary>
        public void Perform()
        {
            this.TouchScreen.DoubleTap(this.ActionLocation);
        }
    }
}
