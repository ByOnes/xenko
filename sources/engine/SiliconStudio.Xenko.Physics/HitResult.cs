﻿// Copyright (c) 2014-2015 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using SiliconStudio.Core.Mathematics;

namespace SiliconStudio.Xenko.Physics
{
    public struct HitResult
    {
        public Collider Collider;

        public Vector3 Normal;

        public Vector3 Point;

        public bool Succeeded;
    }
}