using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Vogen.Tests;

public class TemplatesTests
{

    private class SpecificTypes : IEnumerable<object[]>
    {
        private readonly Type[] _types = new[]
        {
            typeof(bool), typeof(byte), typeof(char), typeof(DateOnly), typeof(DateTime), typeof(DateTimeOffset), typeof(decimal),
            typeof(double), typeof(float), typeof(Guid), typeof(System.Int32), typeof(long), typeof(short), typeof(string),
            typeof(TimeOnly)
        };

        private readonly string[] _technologies = new[]
        {
            "DapperTypeHandler", "EfCoreValueConverter", "LinqToDbValueConverter", "NewtonsoftJsonConverter", "SystemTextJsonConverter",
            "TypeConverter"
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var eachType in _types)
            {
                foreach (var eachTech in _technologies)
                {
                    yield return new object[] { eachType, eachTech };
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private class Anyypes : IEnumerable<object[]>
    {
        private readonly Type[] _types = new[]
        {
            typeof(bool), typeof(byte), typeof(char), typeof(DateOnly), typeof(DateTime), typeof(DateTimeOffset), typeof(decimal),
            typeof(double), typeof(float), typeof(Guid), typeof(System.Int32), typeof(long), typeof(short), typeof(string),
            typeof(TimeOnly)
        };

        private readonly string[] _technologies = new[]
        {
            "DapperTypeHandler", "EfCoreValueConverter", "LinqToDbValueConverter", "NewtonsoftJsonConverterReferenceType", "NewtonsoftJsonConverterValueType", "SystemTextJsonConverter",
            "TypeConverter"
        };
        
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var eachType in _types)
            {
                foreach (var eachTech in _technologies)
                {
                    yield return new object[] { eachType, eachTech };
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    [Theory]
    [ClassData(typeof(SpecificTypes))]
    public void CanGetItemFromSpecificTypeName(Type type, string tech) => Templates.TryGetForSpecificType(type, tech).Should().NotBeNull();

    [Theory]
    [ClassData(typeof(Anyypes))]
#pragma warning disable xUnit1026
    public void CanGetItemForAnyOtherType(Type unused, string tech) => Templates.GetForAnyType(tech).Should().NotBeNull();
#pragma warning restore xUnit1026
}