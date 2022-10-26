﻿using FluentAssertions;
using Shared;
using Vogen;

namespace SnapshotTests;

[UsesVerify]
public class ValueObjectGeneratorTests
{
    [Fact]
    public Task Partial_struct_created_successfully()
    {
        var source = @"using Vogen;
namespace Whatever;

[ValueObject(typeof(int))]
public partial struct CustomerId
{
}";

        return RunTest(source);
    }

    private static Task RunTest(string source) =>
        new SnapshotRunner<ValueObjectGenerator>()
            .WithSource(source)
            .RunOnAllFrameworks();

    [Fact]
    public Task No_namespace() =>
        RunTest(@"using Vogen;

[ValueObject(typeof(int))]
public partial struct CustomerId
{
}");


    [Fact]
    public Task Produces_instances() =>
        RunTest(@"using Vogen;

namespace Whatever;

[ValueObject(typeof(int))]
[Instance(name: ""Unspecified"", value: -1, tripleSlashComment: ""a short description that'll show up in intellisense"")]
[Instance(name: ""Unspecified1"", value: -2)]
[Instance(name: ""Unspecified2"", value: -3, tripleSlashComment: ""<some_xml>whatever</some_xml"")]
[Instance(name: ""Unspecified3"", value: -4)]
[Instance(name: ""Cust42"", value: 42)]
public partial struct CustomerId
{
}
");

    [Fact]
    public Task Validation_with_PascalCased_validate_method() =>
        RunTest(@"using Vogen;

namespace Whatever;

[ValueObject(typeof(int))]
public partial struct CustomerId
{
    private static Validation Validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");

    [Fact]
    public Task Validation_with_camelCased_validate_method() =>
        RunTest(@"using Vogen;

namespace Whatever;

[ValueObject(typeof(int))]
public partial struct CustomerId
{
    private static Validation validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");

    [Fact]
    public Task Instance_names_can_have_reserved_keywords() =>
        RunTest(@"using Vogen;

namespace Whatever;

[ValueObject]
[Instance(name: ""@class"", value: 42)]
[Instance(name: ""@event"", value: 69)]
public partial struct CustomerId
{
    private static Validation validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");

    [Fact]
    public Task Basic_test() =>
        RunTest(@"using Vogen;

namespace Whatever;

[ValueObject(typeof(string))]
public partial struct CustomerId
{
}
");

    [Fact]
    public Task Namespace_names_can_have_reserved_keywords() =>
        RunTest(@"using Vogen;

namespace @double;

[ValueObject]
[Instance(name: ""@class"", value: 42)]
[Instance(name: ""@event"", value: 69)]
[Instance(name: ""@void"", value: 666)]
public partial struct @class
{
    private static Validation validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");
}

[UsesVerify]
public class ValueObjectGeneratorTests_GenerateFromGenericAttribute
{
//#if NET7_0_OR_GREATER
    [Fact]
    public Task Partial_struct_created_successfully()
    {
        var source = @"using Vogen;
namespace Whatever;

[ValueObject<int>]
public partial struct CustomerId
{
}";

        return RunTest(source);
    }

    private static Task RunTest(string source)
    {
        return new SnapshotRunner<ValueObjectGenerator>()
            .WithSource(source)
            .RunOn(TargetFramework.Net7_0);

        // var (diagnostics, output) = TestHelper.GetGeneratedOutput<ValueObjectGenerator>(source);
        //
        // diagnostics.Should().BeEmpty();
        //
        // return Verifier.Verify(output).UseDirectory(SnapshotUtils.GetSnapshotDirectoryName());
    }

    [Fact]
    public Task No_namespace() =>
        RunTest(@"using Vogen;

[ValueObject<int>]
public partial struct CustomerId
{
}");


    [Fact]
    public Task Produces_instances()
    {
        return RunTest(@"using Vogen;

namespace Whatever;

[ValueObject<int>]
[Instance(name: ""Unspecified"", value: -1, tripleSlashComment: ""a short description that'll show up in intellisense"")]
[Instance(name: ""Unspecified1"", value: -2)]
[Instance(name: ""Unspecified2"", value: -3, tripleSlashComment: ""<some_xml>whatever</some_xml"")]
[Instance(name: ""Unspecified3"", value: -4)]
[Instance(name: ""Cust42"", value: 42)]
public partial struct CustomerId
{
}
");
    }

    [Fact]
    public Task Validation_with_PascalCased_validate_method()
    {
        return RunTest(@"using Vogen;

namespace Whatever;

[ValueObject<int>]
public partial struct CustomerId
{
    private static Validation Validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");
    }

    [Fact]
    public Task Validation_with_camelCased_validate_method()
    {
        return RunTest(@"using Vogen;

namespace Whatever;

[ValueObject<int>]
public partial struct CustomerId
{
    private static Validation validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");
    }

    [Fact]
    public Task Instance_names_can_have_reserved_keywords()
    {
        return RunTest(@"using Vogen;

namespace Whatever;

[ValueObject<int>]
[Instance(name: ""@class"", value: 42)]
[Instance(name: ""@event"", value: 69)]
public partial struct CustomerId
{
    private static Validation validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");
    }

    [Fact]
    public Task Namespace_names_can_have_reserved_keywords()
    {
        return RunTest(@"using Vogen;

namespace @double;

[ValueObject<int>]
[Instance(name: ""@class"", value: 42)]
[Instance(name: ""@event"", value: 69)]
[Instance(name: ""@void"", value: 666)]
public partial struct @class
{
    private static Validation validate(int value)
    {
        if (value > 0)
            return Validation.Ok;

        return Validation.Invalid(""must be greater than zero"");
    }
}
");
    }
//#endif
}