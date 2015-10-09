﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Xenko Shader Mixin Code Generator.
// To generate it yourself, please install SiliconStudio.Xenko.VisualStudio.Package .vsix
// and re-save the associated .pdxfx.
// </auto-generated>

using System;
using SiliconStudio.Core;
using SiliconStudio.Xenko.Rendering;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Shaders;
using SiliconStudio.Core.Mathematics;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

namespace SiliconStudio.Xenko.Rendering.Lights
{
    public static partial class LightPointGroupKeys
    {
        public static readonly ParameterKey<Vector3[]> LightPositionWS = ParameterKeys.New<Vector3[]>();
        public static readonly ParameterKey<float[]> LightInvSquareRadius = ParameterKeys.New<float[]>();
        public static readonly ParameterKey<Color3[]> LightColor = ParameterKeys.New<Color3[]>();
    }
}
