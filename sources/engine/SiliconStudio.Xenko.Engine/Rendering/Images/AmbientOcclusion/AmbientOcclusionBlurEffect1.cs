﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Xenko Shader Mixin Code Generator.
// To generate it yourself, please install SiliconStudio.Xenko.VisualStudio.Package .vsix
// and re-save the associated .xkfx.
// </auto-generated>

using System;
using SiliconStudio.Core;
using SiliconStudio.Xenko.Rendering;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Shaders;
using SiliconStudio.Core.Mathematics;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

namespace SiliconStudio.Xenko.Rendering.Images
{
    internal static partial class ShaderMixins
    {
        internal partial class AmbientOcclusionBlurEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "AmbientOcclusionBlurShader", context.GetParam(AmbientOcclusionBlurKeys.Count), context.GetParam(AmbientOcclusionBlurKeys.VerticalBlur));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("AmbientOcclusionBlurEffect", new AmbientOcclusionBlurEffect());
            }
        }
    }
}
