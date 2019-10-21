﻿namespace KraftCore.Tests.Projects.Shared.ExpressionBuilder.Decimal
{
    using System.Linq;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Projects.Shared.ExpressionBuilder.Decimal.Contracts;
    using KraftCore.Tests.Utilities;
    using Xunit;

    // ReSharper disable InconsistentNaming

    /// <summary>
    ///     Unit tests for the dynamic query builder with tests focused on <see cref="decimal"/>? type queries.
    /// </summary>
    public class ExpressionBuilderNullableDecimalTests : ExpressionBuilderTestBase, IExpressionBuilderNullableDecimalTests
    {
        // Decimal

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomHydra.NullableDecimal, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimal == randomHydra.NullableDecimal);
            Assert.DoesNotContain(result, t => t.NullableDecimal != randomHydra.NullableDecimal);
        }

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomHydra.NullableDecimal, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimal != randomHydra.NullableDecimal);
            Assert.DoesNotContain(result, t => t.NullableDecimal == randomHydra.NullableDecimal);
        }

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.LessThan"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Less_Than_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy.Where(t => t.NullableDecimal.HasValue));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomHydra.NullableDecimal, ExpressionOperator.LessThan);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimal < randomHydra.NullableDecimal);
            Assert.DoesNotContain(result, t => t.NullableDecimal >= randomHydra.NullableDecimal);
        }

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.LessThanOrEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Less_Than_Or_Equal_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy.Where(t => t.NullableDecimal.HasValue));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomHydra.NullableDecimal, ExpressionOperator.LessThanOrEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimal <= randomHydra.NullableDecimal);
            Assert.DoesNotContain(result, t => t.NullableDecimal > randomHydra.NullableDecimal);
        }

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.GreaterThan"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Greater_Than_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy.Where(t => t.NullableDecimal.HasValue));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomHydra.NullableDecimal, ExpressionOperator.GreaterThan);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimal > randomHydra.NullableDecimal);
            Assert.DoesNotContain(result, t => t.NullableDecimal <= randomHydra.NullableDecimal);
        }

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.GreaterThanOrEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Greater_Than_Or_Equal_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy.Where(t => t.NullableDecimal.HasValue));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomHydra.NullableDecimal, ExpressionOperator.GreaterThanOrEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimal >= randomHydra.NullableDecimal);
            Assert.DoesNotContain(result, t => t.NullableDecimal < randomHydra.NullableDecimal);
        }

        /// <summary>
        ///     Asserts that an <see cref="decimal"/>? <see cref="ExpressionOperator.Contains"/> on value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Contains_On_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomDecimals = Utilities.GetRandomItems(HydraArmy.Select(t => t.NullableDecimal));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimal), randomDecimals, ExpressionOperator.ContainsOnValue);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomDecimals.Contains(t.NullableDecimal));
            Assert.DoesNotContain(result, t => randomDecimals.Contains(t.NullableDecimal) == false);
        }

        // Decimal Array

        /// <summary>
        ///     Asserts that an array of <see cref="decimal"/>? <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Array_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalArray), randomHydra.NullableDecimalArray, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray));
            Assert.DoesNotContain(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray) == false);
        }

        /// <summary>
        ///     Asserts that an array of <see cref="decimal"/>? <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Array_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalArray), randomHydra.NullableDecimalArray, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray) == false);
            Assert.DoesNotContain(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray));
        }

        /// <summary>
        ///     Asserts that an array of <see cref="decimal"/>? <see cref="ExpressionOperator.Equal"/> expression with an list of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Array_Equality_Expression_With_List_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalArray), randomHydra.NullableDecimalArray.ToList(), ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray));
            Assert.DoesNotContain(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray) == false);
        }

        /// <summary>
        ///     Asserts that an array of <see cref="decimal"/>? <see cref="ExpressionOperator.NotEqual"/> expression with an list of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Array_Inequality_Expression_With_List_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalArray), randomHydra.NullableDecimalArray.ToList(), ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray) == false);
            Assert.DoesNotContain(result, t => t.NullableDecimalArray.SequenceEqual(randomHydra.NullableDecimalArray));
        }

        /// <summary>
        ///     Asserts that an array of <see cref="decimal"/>? <see cref="ExpressionOperator.Contains"/> single value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Array_Contains_Single_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomDecimal = Utilities.GetRandomItem(HydraArmy.Select(t => t.NullableDecimalArray)).FirstOrDefault();
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalArray), randomDecimal, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalArray.Contains(randomDecimal));
            Assert.DoesNotContain(result, t => t.NullableDecimalArray.Contains(randomDecimal) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="decimal"/>? <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Collection_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalCollection), randomHydra.NullableDecimalCollection, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection));
            Assert.DoesNotContain(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="decimal"/>? <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Collection_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalCollection), randomHydra.NullableDecimalCollection, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection) == false);
            Assert.DoesNotContain(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection));
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="decimal"/>? <see cref="ExpressionOperator.Equal"/> expression with an array of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Collection_Equality_Expression_With_Array_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalCollection), randomHydra.NullableDecimalCollection.ToArray(), ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection));
            Assert.DoesNotContain(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="decimal"/>? <see cref="ExpressionOperator.NotEqual"/> expression with an array of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Collection_Inequality_Expression_With_Array_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalCollection), randomHydra.NullableDecimalCollection.ToArray(), ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection) == false);
            Assert.DoesNotContain(result, t => t.NullableDecimalCollection.SequenceEqual(randomHydra.NullableDecimalCollection));
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="decimal"/>? <see cref="ExpressionOperator.Contains"/> single value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Decimal_Collection_Contains_Single_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomDecimal = Utilities.GetRandomItem(HydraArmy.Select(t => t.NullableDecimalCollection)).FirstOrDefault();
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableDecimalCollection), randomDecimal, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableDecimalCollection.Contains(randomDecimal));
            Assert.DoesNotContain(result, t => t.NullableDecimalCollection.Contains(randomDecimal) == false);
        }
    }
}