﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;
using SiliconStudio.Shaders.Ast;
using SiliconStudio.Shaders.Parser;
using SiliconStudio.Shaders.Utility;
using SiliconStudio.Shaders.Visitor;

namespace SiliconStudio.Shaders.Analysis
{
    /// <summary>
    /// Base class for analysis.
    /// </summary>
    public abstract class AnalysisBase : ShaderRewriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisBase"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        protected AnalysisBase(ParsingResult result) : base(true, true)
        {
            ParsingResult = result;
        }

        /// <summary>
        /// Gets the parsing result.
        /// </summary>
        public ParsingResult ParsingResult { get; private set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Logs an Error with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="span">The span.</param>
        protected void Error(MessageCode message, SourceSpan span)
        {
            ParsingResult.Error(message, span);
        }

        /// <summary>
        /// Logs an Error with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="span">The span.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Error(MessageCode message, SourceSpan span, params object[] parameters)
        {
            ParsingResult.Error(message, span, parameters);
        }

        /// <summary>
        /// Logs an Info with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="span">The span.</param>
        protected void Info(string message, SourceSpan span)
        {
            ParsingResult.Info(message, span);
        }

        /// <summary>
        /// Logs an Info with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="span">The span.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Info(string message, SourceSpan span, params object[] parameters)
        {
            ParsingResult.Info(message, span, parameters);
        }

        /// <summary>
        /// Logs an Warning with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="span">The span.</param>
        protected void Warning(MessageCode message, SourceSpan span)
        {
            ParsingResult.Warning(message, span);
        }

        /// <summary>
        /// Logs an Warning with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="span">The span.</param>
        /// <param name="parameters">The parameters.</param>
        protected void Warning(MessageCode message, SourceSpan span, params object[] parameters)
        {
            ParsingResult.Warning(message, span, parameters);
        }

        /// <summary>
        /// Gets the type of the binary implicit conversion.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="isBinaryOperator">if set to <c>true</c> [is binary operator].</param>
        /// <returns>
        /// The implicit conversion between between to two types
        /// </returns>
        protected virtual TypeBase GetBinaryImplicitConversionType(SourceSpan span, TypeBase left, TypeBase right, bool isBinaryOperator)
        {
            var result = CastHelper.GetBinaryImplicitConversionType(left, right, isBinaryOperator);

            if (result == null)
                Error(MessageCode.ErrorBinaryTypeDeduction, span, left, right);

            return result;
        }

        /// <summary>
        /// Gets the type of the binary implicit conversion.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The implicit conversion between between to two types</returns>
        protected virtual TypeBase GetMultiplyImplicitConversionType(SourceSpan span, TypeBase left, TypeBase right)
        {
            var result = CastHelper.GetMultiplyImplicitConversionType(left.ResolveType(), right.ResolveType());

            if (result == null)
                Error(MessageCode.ErrorBinaryTypeDeduction, span, left, right);

            return result;
        }

        /// <summary>
        /// Gets the type of the binary implicit conversion.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The implicit conversion between between to two types</returns>
        protected virtual TypeBase GetDivideImplicitConversionType(SourceSpan span, TypeBase left, TypeBase right)
        {
            var result = CastHelper.GetDivideImplicitConversionType(left.ResolveType(), right.ResolveType());

            if (result == null)
                Error(MessageCode.ErrorBinaryTypeDeduction, span, left, right);

            return result;
        }

        /// <summary>
        /// Gets the type of the binary implicit scalar conversion.
        /// </summary>
        /// <param name="span">The span.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The implicit conversion between the two scalar types
        /// </returns>
        protected ScalarType GetBinaryImplicitScalarConversionType(SourceSpan span, TypeBase left, TypeBase right)
        {
            var result = CastHelper.GetBinaryImplicitScalarConversionType(left, right);

            if (result == null)
                Error(MessageCode.ErrorScalarTypeConversion, span, left, right);
            return result;
        }
    }
}
