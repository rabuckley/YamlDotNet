﻿// This file is part of YamlDotNet - A .NET library for YAML.
// Copyright (c) Antoine Aubry and contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using YamlDotNet.Core;
using YamlDotNet.Core.Tokens;
using Version = YamlDotNet.Core.Version;

namespace YamlDotNet.Test.Core
{
    public class TokenHelper
    {
        protected static StreamStart StreamStart
        {
            get { return new StreamStart(); }
        }

        protected static StreamEnd StreamEnd
        {
            get { return new StreamEnd(); }
        }

        protected static DocumentStart DocumentStart
        {
            get { return new DocumentStart(); }
        }

        protected static DocumentEnd DocumentEnd
        {
            get { return new DocumentEnd(); }
        }

        protected static VersionDirective VersionDirective(int major, int minor)
        {
            return new VersionDirective(new Version(major, minor));
        }

        protected static TagDirective TagDirective(string handle, string prefix)
        {
            return new TagDirective(handle, prefix);
        }

        protected static Tag Tag(string handle, string suffix)
        {
            return new Tag(handle, suffix);
        }

        protected static Scalar PlainScalar(string text)
        {
            return new Scalar(text, ScalarStyle.Plain);
        }

        protected static Scalar SingleQuotedScalar(string text)
        {
            return new Scalar(text, ScalarStyle.SingleQuoted);
        }

        protected static Scalar DoubleQuotedScalar(string text)
        {
            return new Scalar(text, ScalarStyle.DoubleQuoted);
        }

        protected static Scalar LiteralScalar(string text)
        {
            return new Scalar(text, ScalarStyle.Literal);
        }

        protected static Scalar FoldedScalar(string text)
        {
            return new Scalar(text, ScalarStyle.Folded);
        }

        protected static FlowSequenceStart FlowSequenceStart
        {
            get { return new FlowSequenceStart(); }
        }

        protected static FlowSequenceEnd FlowSequenceEnd
        {
            get { return new FlowSequenceEnd(); }
        }

        protected static BlockSequenceStart BlockSequenceStart
        {
            get { return new BlockSequenceStart(); }
        }

        protected static FlowMappingStart FlowMappingStart
        {
            get { return new FlowMappingStart(); }
        }

        protected static FlowMappingEnd FlowMappingEnd
        {
            get { return new FlowMappingEnd(); }
        }

        protected static BlockMappingStart BlockMappingStart
        {
            get { return new BlockMappingStart(); }
        }

        protected static Key Key
        {
            get { return new Key(); }
        }

        protected static Value Value
        {
            get { return new Value(); }
        }

        protected static FlowEntry FlowEntry
        {
            get { return new FlowEntry(); }
        }

        protected static BlockEntry BlockEntry
        {
            get { return new BlockEntry(); }
        }

        protected static BlockEnd BlockEnd
        {
            get { return new BlockEnd(); }
        }

        protected static Anchor Anchor(string anchor)
        {
            return new Anchor(new AnchorName(anchor));
        }

        protected static AnchorAlias AnchorAlias(string alias)
        {
            return new AnchorAlias(new AnchorName(alias));
        }

        protected static Error Error(string value)
        {
            return new Error(value, new Mark(), new Mark());
        }

        protected static Comment StandaloneComment(string text)
        {
            return new Comment(text, false);
        }


        protected static Comment InlineComment(string text)
        {
            return new Comment(text, true);
        }
    }
}
