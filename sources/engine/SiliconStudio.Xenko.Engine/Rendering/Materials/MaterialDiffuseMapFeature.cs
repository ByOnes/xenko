// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using System;
using System.Collections.Generic;

using SiliconStudio.Core;
using SiliconStudio.Core.Annotations;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Rendering.Materials.ComputeColors;
using SiliconStudio.Xenko.Shaders;

namespace SiliconStudio.Xenko.Rendering.Materials
{
    /// <summary>
    /// A Diffuse map for the diffuse material feature.
    /// </summary>
    [DataContract("MaterialDiffuseMapFeature")]
    [Display("Diffuse Map")]
    public class MaterialDiffuseMapFeature : MaterialFeature, IMaterialDiffuseFeature, IMaterialStreamProvider
    {
        public static readonly MaterialStreamDescriptor DiffuseStream = new MaterialStreamDescriptor("Diffuse", "matDiffuse", MaterialKeys.DiffuseValue.PropertyType);
        public static readonly MaterialStreamDescriptor ColorBaseStream = new MaterialStreamDescriptor("Color Base", "matColorBase", MaterialKeys.DiffuseValue.PropertyType);

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialDiffuseMapFeature"/> class.
        /// </summary>
        public MaterialDiffuseMapFeature()
        {
            DiffuseMap = new ComputeTextureColor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialDiffuseMapFeature"/> class.
        /// </summary>
        /// <param name="diffuseMap">The diffuse map.</param>
        public MaterialDiffuseMapFeature(IComputeColor diffuseMap)
        {
            if (diffuseMap == null) throw new ArgumentNullException("diffuseMap");
            DiffuseMap = diffuseMap;
        }

        /// <summary>
        /// Gets or sets the diffuse map.
        /// </summary>
        /// <value>The diffuse map.</value>
        [Display("Diffuse Map")]
        [NotNull]
        [DataMemberCustomSerializer]
        public IComputeColor DiffuseMap { get; set; }

        public override void VisitFeature(MaterialGeneratorContext context)
        {
            if (DiffuseMap != null)
            {
                var computeColorSource = DiffuseMap.GenerateShaderSource(context, new MaterialComputeColorKeys(MaterialKeys.DiffuseMap, MaterialKeys.DiffuseValue, Color.White));
                var mixin = new ShaderMixinSource();
                mixin.Mixins.Add(new ShaderClassSource("MaterialSurfaceDiffuse"));
                mixin.AddComposition("diffuseMap", computeColorSource);
                context.UseStream(MaterialShaderStage.Pixel, DiffuseStream.Stream);
                context.UseStream(MaterialShaderStage.Pixel, ColorBaseStream.Stream);
                context.AddSurfaceShader(MaterialShaderStage.Pixel, mixin);
            }
        }

        public IEnumerable<MaterialStreamDescriptor> GetStreams()
        {
            yield return ColorBaseStream;
            yield return DiffuseStream;
        }
    }
}