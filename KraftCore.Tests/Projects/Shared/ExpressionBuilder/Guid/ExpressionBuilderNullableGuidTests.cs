﻿namespace KraftCore.Tests.Projects.Shared.ExpressionBuilder.Guid
{
    using System;
    using System.Linq;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Projects.Shared.ExpressionBuilder.Guid.Contracts;
    using KraftCore.Tests.Utilities;
    using Xunit;
    using Xunit.Abstractions;

    // ReSharper disable InconsistentNaming

    /// <summary>
    ///     Unit tests for the dynamic query builder with tests focused on <see cref="Guid"/>? type queries.
    /// </summary>
    public class ExpressionBuilderNullableGuidTests : ExpressionBuilderTestBase, IExpressionBuilderNullableGuidTests
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionBuilderNullableGuidTests"/> class.
        /// </summary>
        /// <param name="testOutput">
        ///     The class responsible for providing test output.
        /// </param>
        public ExpressionBuilderNullableGuidTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }

        // Guid

        /// <summary>
        ///     Asserts that an <see cref="Guid"/>? <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuid), randomHydra.NullableGuid, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuid == randomHydra.NullableGuid);
            Assert.DoesNotContain(result, t => t.NullableGuid != randomHydra.NullableGuid);
        }

        /// <summary>
        ///     Asserts that an <see cref="Guid"/>? <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuid), randomHydra.NullableGuid, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuid != randomHydra.NullableGuid);
            Assert.DoesNotContain(result, t => t.NullableGuid == randomHydra.NullableGuid);
        }

        /// <summary>
        ///     Asserts that an <see cref="Guid"/>? <see cref="ExpressionOperator.Contains"/> on value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Contains_On_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomGuids = Utilities.GetRandomItems(HydraArmy.Select(t => t.NullableGuid));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuid), randomGuids, ExpressionOperator.ContainsOnValue);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomGuids.Contains(t.NullableGuid));
            Assert.DoesNotContain(result, t => randomGuids.Contains(t.NullableGuid) == false);
        }

        // Guid Array

        /// <summary>
        ///     Asserts that an array of <see cref="Guid"/>? <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Array_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidArray), randomHydra.NullableGuidArray, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray));
            Assert.DoesNotContain(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray) == false);
        }

        /// <summary>
        ///     Asserts that an array of <see cref="Guid"/>? <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Array_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidArray), randomHydra.NullableGuidArray, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray) == false);
            Assert.DoesNotContain(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray));
        }

        /// <summary>
        ///     Asserts that an array of <see cref="Guid"/>? <see cref="ExpressionOperator.Equal"/> expression with an list of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Array_Equality_Expression_With_List_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidArray), randomHydra.NullableGuidArray.ToList(), ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray));
            Assert.DoesNotContain(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray) == false);
        }

        /// <summary>
        ///     Asserts that an array of <see cref="Guid"/>? <see cref="ExpressionOperator.NotEqual"/> expression with an list of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Array_Inequality_Expression_With_List_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidArray), randomHydra.NullableGuidArray.ToList(), ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray) == false);
            Assert.DoesNotContain(result, t => t.NullableGuidArray.SequenceEqual(randomHydra.NullableGuidArray));
        }

        /// <summary>
        ///     Asserts that an array of <see cref="Guid"/>? <see cref="ExpressionOperator.Contains"/> single value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Array_Contains_Single_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomGuid = Utilities.GetRandomItem(HydraArmy.Select(t => t.NullableGuidArray)).FirstOrDefault();
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidArray), randomGuid, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidArray.Contains(randomGuid));
            Assert.DoesNotContain(result, t => t.NullableGuidArray.Contains(randomGuid) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="Guid"/>? <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Collection_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidCollection), randomHydra.NullableGuidCollection, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection));
            Assert.DoesNotContain(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="Guid"/>? <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Collection_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidCollection), randomHydra.NullableGuidCollection, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection) == false);
            Assert.DoesNotContain(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection));
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="Guid"/>? <see cref="ExpressionOperator.Equal"/> expression with an array of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Collection_Equality_Expression_With_Array_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidCollection), randomHydra.NullableGuidCollection.ToArray(), ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection));
            Assert.DoesNotContain(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="Guid"/>? <see cref="ExpressionOperator.NotEqual"/> expression with an array of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Collection_Inequality_Expression_With_Array_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidCollection), randomHydra.NullableGuidCollection.ToArray(), ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection) == false);
            Assert.DoesNotContain(result, t => t.NullableGuidCollection.SequenceEqual(randomHydra.NullableGuidCollection));
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="Guid"/>? <see cref="ExpressionOperator.Contains"/> single value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_Nullable_Guid_Collection_Contains_Single_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomGuid = Utilities.GetRandomItem(HydraArmy.Select(t => t.NullableGuidCollection)).FirstOrDefault();
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.NullableGuidCollection), randomGuid, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.NullableGuidCollection.Contains(randomGuid));
            Assert.DoesNotContain(result, t => t.NullableGuidCollection.Contains(randomGuid) == false);
        }
    }
}
