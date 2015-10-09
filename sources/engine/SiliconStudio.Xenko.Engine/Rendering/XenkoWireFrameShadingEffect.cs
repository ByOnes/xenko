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

using SiliconStudio.Xenko.Rendering.Data;
using SiliconStudio.Xenko.Shaders.Compiler;
namespace SiliconStudio.Xenko.Rendering
{
    internal static partial class ShaderMixins
    {
        internal partial class XenkoWireFrameShadingEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "XenkoEffectBase");
                context.Mixin(mixin, "MaterialFrontBackBlendShader", context.GetParam(MaterialFrontBackBlendShaderKeys.UseNormalBackFace));
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("XenkoWireFrameShadingEffect", new XenkoWireFrameShadingEffect());
            }
        }
    }
}
