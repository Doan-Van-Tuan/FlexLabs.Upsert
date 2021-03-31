﻿using System;
using System.Linq.Expressions;
using FlexLabs.EntityFrameworkCore.Upsert.Internal;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace FlexLabs.EntityFrameworkCore.Upsert.Tests.Internal
{
    public static class ExpressionAssertionExtensions
    {
        public static AndWhichConstraint<ObjectAssertions, KnownExpression> BeKnownExpression(this ObjectAssertions assertions, ExpressionType expressionType)
        {
            using var _ = new AssertionScope();
            var result = assertions.BeOfType<KnownExpression>();
            result.Subject.ExpressionType.Should().Be(expressionType);
            return result;
        }

        public static AndWhichConstraint<ObjectAssertions, KnownExpression> HaveKnownExpression(this AndWhichConstraint<ObjectAssertions, KnownExpression> assertions, Func<KnownExpression, IKnownValue> property, ExpressionType expressionType,
            Action<AndWhichConstraint<ObjectAssertions, KnownExpression>> and = null)
        {
            var expressionConstraint = property(assertions.Subject).Should().BeKnownExpression(expressionType);
            and?.Invoke(expressionConstraint);
            return assertions;
        }

        public static AndWhichConstraint<ObjectAssertions, KnownExpression> HavePropertyValue(this AndWhichConstraint<ObjectAssertions, KnownExpression> assertions, Func<KnownExpression, IKnownValue> property, string name, bool isLeftParam)
        {
            property(assertions.Subject).Should().BePropertyValue(name, isLeftParam);
            return assertions;
        }

        public static AndWhichConstraint<ObjectAssertions, KnownExpression> HaveConstantValue(this AndWhichConstraint<ObjectAssertions, KnownExpression> assertions, Func<KnownExpression, IKnownValue> property, object expectedValue)
        {
            property(assertions.Subject).Should().BeConstantValue(expectedValue);
            return assertions;
        }

        public static AndWhichConstraint<ObjectAssertions, PropertyValue> BePropertyValue(this ObjectAssertions assertions, string name, bool isLeftParam)
        {
            using var _ = new AssertionScope();
            var result = assertions.Subject.Should().BeOfType<PropertyValue>();
            result.Subject.PropertyName.Should().Be(name);
            result.Subject.IsLeftParameter.Should().Be(isLeftParam);
            return result;
        }

        public static AndWhichConstraint<ObjectAssertions, ConstantValue> BeConstantValue(this ObjectAssertions assertions, object expectedValue)
        {
            using var _ = new AssertionScope();
            var result = assertions.Subject.Should().BeOfType<ConstantValue>();
            result.Subject.Value.Should().Be(expectedValue);
            return result;
        }
    }
}
