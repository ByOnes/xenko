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

namespace SiliconStudio.Xenko.Rendering.Images
{
    internal static partial class ShaderMixins
    {
        internal partial class ToneMapEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "ToneMapShader", context.GetParam(ToneMapKeys.AutoKey), context.GetParam(ToneMapKeys.AutoExposure));

                {
                    var __mixinToCompose__ = context.GetParam(ColorTransformKeys.Shader);
                    var __subMixin = new ShaderMixinSource();
                    context.PushComposition(mixin, "ToneMapOperator", __subMixin);
                    context.Mixin(__subMixin, __mixinToCompose__);
                    context.PopComposition();
                }
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("ToneMapEffect", new ToneMapEffect());
            }
        }
    }
}
