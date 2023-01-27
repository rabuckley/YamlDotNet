// This file is part of YamlDotNet - A .NET library for YAML.
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

using BenchmarkDotNet.Attributes;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace YamlDotNet.Benchmark;

[MemoryDiagnoser]
public class YamlDeserializationBenchmarks
{
    private static readonly IDeserializer s_deserializer = new DeserializerBuilder()
        .WithNamingConvention(LowerCaseNamingConvention.Instance).Build();

    private const string Path = "Resources/nav-items.yml";

    [Benchmark]
    public void Deserialize_Stream()
    {
        using var stream = File.Open(Path, FileMode.Open);
        using var textReader = new StreamReader(stream);
        _ = s_deserializer.Deserialize<NavItem[]>(textReader);
    }

    [Benchmark]
    public void Deserialize_String()
    {
        var yaml = File.ReadAllText(Path);
        _ = s_deserializer.Deserialize<NavItem[]>(yaml);
    }

    private sealed class NavItem
    {
        public string Name { get; init; } = default!;
        public string Path { get; init; } = default!;
        public IList<NavItem> Items { get; init; } = default!;
    }
}
