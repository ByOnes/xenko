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
using SiliconStudio.Xenko.Rendering.Skyboxes;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Shaders;
using SiliconStudio.Core.Mathematics;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

using SiliconStudio.Xenko.Rendering.Data;
using SiliconStudio.Xenko.Rendering.Materials;
namespace SiliconStudio.Xenko.Rendering.Skyboxes
{
    internal static partial class ShaderMixins
    {
        internal partial class SkyboxEffect  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSource mixin, ShaderMixinContext context)
            {
                context.Mixin(mixin, "SkyboxShader");
                if (context.GetParam(SkyboxKeys.Shader) != null)
                {

                    {
                        var __subMixin = new ShaderMixinSource() { Parent = mixin };
                        context.PushComposition(mixin, "skyboxColor", __subMixin);
                        context.Mixin(__subMixin, context.GetParam(SkyboxKeys.Shader));
                        context.PopComposition();
                    }
                }
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("SkyboxEffect", new SkyboxEffect());
            }
        }
    }
}
