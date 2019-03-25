﻿using Netlenium.WebDriver.Interactions.Internal;

namespace Netlenium.WebDriver.Interactions
{
    /// <summary>
    /// Presses a touch screen at a given location.
    /// </summary>
    internal class ScreenMoveAction : TouchAction, IAction
    {
        private int x;
        private int y;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenMoveAction"/> class.
        /// </summary>
        /// <param name="touchScreen">The <see cref="ITouchScreen"/> with which the action will be performed.</param>
        /// <param name="x">The x coordinate relative to the view port.</param>
        /// <param name="y">The y coordinate relative to the view port.</param>
        public ScreenMoveAction(ITouchScreen touchScreen, int x, int y)
            : base(touchScreen, null)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Performs the action.
        /// </summary>
        public void Perform()
        {
            this.TouchScreen.Move(this.x, this.y);
        }
    }
}
