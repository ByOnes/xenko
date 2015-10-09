﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;

namespace SiliconStudio.TextureConverter.Requests
{
    /// <summary>
    /// Request to load a texture, either from a file, or from memory with an <see cref="TexImage"/> or a <see cref="SiliconStudio.Xenko.Graphics.Image"/>
    /// </summary>
    internal class LoadingRequest : IRequest
    {
        /// <summary>
        /// The different loading mode : TexImage, file, Xenko Image
        /// </summary>
        public enum LoadingMode
        {
            TexImage,
            PdxImage,
            FilePath,
        }

        public override RequestType Type { get { return RequestType.Loading; } }

        /// <summary>
        /// The mode used by the request
        /// </summary>
        public LoadingMode Mode { set; get; }
        
        /// <summary>
        /// The file path
        /// </summary>
        public String FilePath { set; get; }
        
        /// <summary>
        /// The TexImage to be loaded
        /// </summary>
        public TexImage Image { set; get; }

        /// <summary>
        /// The Xenko Image to be loaded
        /// </summary>
        public Xenko.Graphics.Image PdxImage;

        /// <summary>
        /// Indicate if we should keep the original mip-maps during the load
        /// </summary>
        public bool KeepMipMap { get; set; }

        /// <summary>
        /// Indicate if the input file should be loaded as an sRGB file.
        /// </summary>
        public bool LoadAsSRgb { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingRequest"/> class to load a texture from a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="loadAsSRgb">Indicate if the input file should be loaded as in sRGB file</param>
        public LoadingRequest(String filePath, bool loadAsSRgb)
        {
            FilePath = filePath;
            Mode = LoadingMode.FilePath;
            LoadAsSRgb = loadAsSRgb;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingRequest"/> class to load a texture from a <see cref="TexImage"/> instance.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="loadAsSRgb">Indicate if the input file should be loaded as in sRGB file</param>
        public LoadingRequest(TexImage image, bool loadAsSRgb = false)
        {
            Image = image;
            Mode = LoadingMode.TexImage;
            LoadAsSRgb = loadAsSRgb;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingRequest"/> class to load a texture from a <see cref="SiliconStudio.Xenko.Graphics.Image"/> instance.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="loadAsSRgb">Indicate if the input file should be loaded as in sRGB file</param>
        public LoadingRequest(Xenko.Graphics.Image image, bool loadAsSRgb = false)
        {
            PdxImage = image;
            Mode = LoadingMode.PdxImage;
            LoadAsSRgb = loadAsSRgb;
        }
    }
}
