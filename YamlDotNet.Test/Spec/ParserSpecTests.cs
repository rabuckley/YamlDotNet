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

using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace YamlDotNet.Test.Spec
{
    public sealed class ParserSpecTests
    {
        private sealed class ParserSpecTestsData : SpecTestsData
        {
            protected override List<string> IgnoredSuites { get; } = ignoredSuites;
        }

        private static readonly List<string> ignoredSuites = new List<string>
        {
            "L383" // this is a multi document test, yamldotnet does not support it.
        };

        private static readonly List<string> knownFalsePositives = new List<string>
        {
            // no false-positives known as of https://github.com/yaml/yaml-test-suite/releases/tag/data-2020-02-11
        };

        private static readonly List<string> knownParserDesyncInErrorCases = new List<string>
        {
            "C2SP" // this is supposed to error out, which it does, just not in the same spot.
        };

        [Theory, ClassData(typeof(ParserSpecTestsData))]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
        public void ConformsWithYamlSpec(string name, string description, string inputFile, string expectedEventFile, bool error, bool quoting)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
        {
            // we don't care about quoting for this test.

            var expectedResult = File.ReadAllText(expectedEventFile)
                .Replace("+MAP {}", "+MAP")
                .Replace("+SEQ []", "+SEQ");

            using var writer = new StringWriter();
            try
            {
                using var reader = File.OpenText(inputFile);
                new LibYamlEventStream(new Parser(reader)).WriteTo(writer);
            }
            catch (Exception ex)
            {
                Assert.True(error, $"Unexpected spec failure ({name}).\n{description}\nExpected:\n{expectedResult}\nActual:\n[Writer Output]\n{writer}\n[Exception]\n{ex}");

                if (error)
                {
                    Debug.Assert(!knownFalsePositives.Contains(name), $"Spec test '{name}' passed but present in '{nameof(knownFalsePositives)}' list. Consider removing it from the list.");

                    try
                    {
                        Assert.Equal(expectedResult, writer.ToString(), ignoreLineEndingDifferences: true);
                        Debug.Assert(!knownParserDesyncInErrorCases.Contains(name), $"Spec test '{name}' passed but present in '{nameof(knownParserDesyncInErrorCases)}' list. Consider removing it from the list.");
                    }
                    catch (EqualException)
                    {
                        // In some error cases, YamlDotNet's parser output is in desync with what is expected by the spec.
                        // Throw, if it is not a known case.

                        if (!knownParserDesyncInErrorCases.Contains(name))
                        {
                            throw;
                        }
                    }
                }

                return;
            }

            try
            {
                Assert.Equal(expectedResult, writer.ToString(), ignoreLineEndingDifferences: true);
                Debug.Assert(!ignoredSuites.Contains(name), $"Spec test '{name}' passed but present in '{nameof(ignoredSuites)}' list. Consider removing it from the list.");
            }
            catch (EqualException)
            {
                // In some cases, YamlDotNet's parser/scanner is unexpectedly *not* erroring out.
                // Throw, if it is not a known case.

                if (!(error && knownFalsePositives.Contains(name)))
                {
                    throw;
                }
            }
        }
    }
}
