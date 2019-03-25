﻿using System;
using System.Collections.Generic;

namespace Netlenium.WebDriver.Interactions.Internal
{
    /// <summary>
    /// Defines an action for keyboard interaction with the browser using a single modifier key.
    /// </summary>
    internal class SingleKeyAction : KeyboardAction
    {
        private static readonly List<string> ModifierKeys = new List<string>() { Keys.Shift, Keys.Control, Keys.Alt, Keys.Meta,
                                                                                 Keys.Command, Keys.LeftAlt, Keys.LeftControl, Keys.LeftShift};
        private string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleKeyAction"/> class.
        /// </summary>
        /// <param name="keyboard">The <see cref="IKeyboard"/> to use in performing the action.</param>
        /// <param name="mouse">The <see cref="IMouse"/> to use in setting focus to the element on which to perform the action.</param>
        /// <param name="actionTarget">An <see cref="ILocatable"/> object providing the element on which to perform the action.</param>
        /// <param name="key">The modifier key (<see cref="Keys.Shift"/>, <see cref="Keys.Control"/>, <see cref="Keys.Alt"/>) to use in the action.</param>
        protected SingleKeyAction(IKeyboard keyboard, IMouse mouse, ILocatable actionTarget, string key)
            : base(keyboard, mouse, actionTarget)
        {
            if (!ModifierKeys.Contains(key))
            {
                throw new ArgumentException("key must be a modifier key (Keys.Shift, Keys.Control, Keys.Alt, Keys.Meta, Keys.Command, Keys.LeftAlt, Keys.LeftControl, Keys.LeftShift)", "key");
            }

            this.key = key;
        }

        /// <summary>
        /// Gets the key with which to perform the action.
        /// </summary>
        protected string Key
        {
            get { return this.key; }
        }
    }
}
