// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace body_array.Models
{
    internal static class FooEnumExtensions
    {
        public static string ToSerialString(this FooEnum value) => value switch
        {
            FooEnum.Foo1 => "foo1",
            FooEnum.Foo2 => "foo2",
            FooEnum.Foo3 => "foo3",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown FooEnum value.")
        };

        public static FooEnum ToFooEnum(this string value) => value switch
        {
            "foo1" => FooEnum.Foo1,
            "foo2" => FooEnum.Foo2,
            "foo3" => FooEnum.Foo3,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown FooEnum value.")
        };
    }
}
