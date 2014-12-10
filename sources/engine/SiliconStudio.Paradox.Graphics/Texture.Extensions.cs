﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using System;

namespace SiliconStudio.Paradox.Graphics
{
    public static class TextureExtensions
    {

        public static Texture ToTextureView(this Texture texture, ViewType type, int arraySlice, int mipLevel)
        {
            var viewDescription = texture.ViewDescription;
            viewDescription.Type = type;
            viewDescription.ArraySlice = arraySlice;
            viewDescription.MipLevel = mipLevel;
            return texture.ToTextureView(viewDescription);
        }

        /// <summary>
        /// Gets a view on this depth stencil texture as a readonly depth stencil texture.
        /// </summary>
        /// <returns>A new texture object that is bouded to the requested view.</returns>
        public static Texture ToDepthStencilReadOnlyTexture(this Texture texture)
        {
            if (!texture.IsDepthStencil)
                throw new NotSupportedException("This texture is not a valid depth stencil texture");

            var viewDescription = texture.ViewDescription;
            viewDescription.Flags = TextureFlags.DepthStencilReadOnly;
            return texture.ToTextureView(viewDescription);
        }

        /// <summary>
        /// Creates a new texture that can be used as a ShaderResource from an existing depth texture.
        /// </summary>
        /// <returns></returns>
        public static Texture CreateDepthTextureCompatible(this Texture texture)
        {
            if (!texture.IsDepthStencil)
                throw new NotSupportedException("This texture is not a valid depth stencil texture");

            var description = texture.Description;
            description.Format = (PixelFormat)Texture.ComputeShaderResourceFormatFromDepthFormat(description.Format);
            if (description.Format == PixelFormat.None)
                throw new NotSupportedException("This depth stencil format is not supported");

            description.Flags = TextureFlags.ShaderResource;
            return Texture.New(texture.GraphicsDevice, description);
        }

        public static Texture EnsureRenderTarget(this Texture texture)
        {
            if (texture != null && !texture.IsRenderTarget)
            {
                throw new ArgumentException("Texture must be a RenderTarget", "texture");
            }
            return texture;
        }
    }
}