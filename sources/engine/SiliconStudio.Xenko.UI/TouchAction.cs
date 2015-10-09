﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
namespace SiliconStudio.Xenko.UI
{
    /// <summary>
    /// Describes the action of a specific touch point.
    /// </summary>
    public enum TouchAction
    {
        /// <summary>
        /// The act of putting a finger onto the screen.
        /// </summary>
        Down,

        /// <summary>
        /// The act of dragging a finger across the screen.
        /// </summary>
        Move,

        /// <summary>
        /// The act of lifting a finger off of the screen.
        /// </summary>
        Up,
    }
}