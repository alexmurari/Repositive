﻿namespace KraftCore.Tests.Projects.Shared.ExpressionBuilder.String
{
    using System.Linq;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Projects.Shared.ExpressionBuilder;
    using KraftCore.Tests.Projects.Shared.ExpressionBuilder.String.Contracts;
    using KraftCore.Tests.Utilities;
    using Xunit;

    /// <summary>
    ///     Unit tests for the dynamic query builder with tests focused on <see cref="string"/> type queries.
    /// </summary>
    public class ExpressionBuilderStringTests : ExpressionBuilderTestBase, IExpressionBuilderStringTests
    {
        // String

        /// <summary>
        ///     Asserts that an <see cref="string"/> <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.FullName), randomHydra.FullName, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName == randomHydra.FullName);
            Assert.DoesNotContain(result, t => t.FullName != randomHydra.FullName);
        }

        /// <summary>
        ///     Asserts that an <see cref="string"/> <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.FullName), randomHydra.FullName, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName != randomHydra.FullName);
            Assert.DoesNotContain(result, t => t.FullName == randomHydra.FullName);
        }

        /// <summary>
        ///     Asserts that an <see cref="string"/> <see cref="ExpressionOperator.StartsWith"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Starts_With_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.FullName), randomHydra.FirstName, ExpressionOperator.StartsWith);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.StartsWith(randomHydra.FirstName));
            Assert.DoesNotContain(result, t => t.FullName.StartsWith(randomHydra.FirstName) == false);
        }

        /// <summary>
        ///     Asserts that an <see cref="string"/> <see cref="ExpressionOperator.EndsWith"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Ends_With_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.FullName), randomHydra.LastName, ExpressionOperator.EndsWith);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.EndsWith(randomHydra.LastName));
            Assert.DoesNotContain(result, t => t.FullName.EndsWith(randomHydra.LastName) == false);
        }

        /// <summary>
        ///     Asserts that an <see cref="string"/> <see cref="ExpressionOperator.Contains"/> on value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Contains_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomString = Utilities.GetRandomItem(HydraArmy.Select(t => t.FirstName));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.FullName), randomString, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomString.Contains(t.FirstName));
            Assert.DoesNotContain(result, t => randomString.Contains(t.FirstName) == false);
        }

        /// <summary>
        ///     Asserts that an <see cref="string"/> <see cref="ExpressionOperator.Contains"/> on value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Contains_On_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomStrings = Utilities.GetRandomItems(HydraArmy.Select(t => t.FullName));
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.FullName), randomStrings, ExpressionOperator.ContainsOnValue);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomStrings.Contains(t.FullName));
            Assert.DoesNotContain(result, t => randomStrings.Contains(t.FullName) == false);
        }

        // String Array

        /// <summary>
        ///     Asserts that an array of <see cref="string"/> <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Array_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringArray), randomHydra.StringArray, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray));
            Assert.DoesNotContain(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray) == false);
        }

        /// <summary>
        ///     Asserts that an array of <see cref="string"/> <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Array_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringArray), randomHydra.StringArray, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray) == false);
            Assert.DoesNotContain(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray));
        }

        /// <summary>
        ///     Asserts that an array of <see cref="string"/> <see cref="ExpressionOperator.Equal"/> expression with an list of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Array_Equality_Expression_With_List_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringArray), randomHydra.StringArray.ToList(), ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray));
            Assert.DoesNotContain(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray) == false);
        }

        /// <summary>
        ///     Asserts that an array of <see cref="string"/> <see cref="ExpressionOperator.NotEqual"/> expression with an list of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Array_Inequality_Expression_With_List_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringArray), randomHydra.StringArray.ToList(), ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray) == false);
            Assert.DoesNotContain(result, t => t.StringArray.SequenceEqual(randomHydra.StringArray));
        }

        /// <summary>
        ///     Asserts that an array of <see cref="string"/> <see cref="ExpressionOperator.Contains"/> single value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Array_Contains_Single_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomString = Utilities.GetRandomItem(HydraArmy.Select(t => t.StringArray)).FirstOrDefault();
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringArray), randomString, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringArray.Contains(randomString));
            Assert.DoesNotContain(result, t => t.StringArray.Contains(randomString) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="string"/> <see cref="ExpressionOperator.Equal"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Collection_Equality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringCollection), randomHydra.StringCollection, ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection));
            Assert.DoesNotContain(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="string"/> <see cref="ExpressionOperator.NotEqual"/> expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Collection_Inequality_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringCollection), randomHydra.StringCollection, ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection) == false);
            Assert.DoesNotContain(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection));
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="string"/> <see cref="ExpressionOperator.Equal"/> expression with an array of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Collection_Equality_Expression_With_Array_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringCollection), randomHydra.StringCollection.ToArray(), ExpressionOperator.Equal);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection));
            Assert.DoesNotContain(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection) == false);
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="string"/> <see cref="ExpressionOperator.NotEqual"/> expression with an array of values is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Collection_Inequality_Expression_With_Array_Value_Is_Generated_Correctly()
        {
            // Arrange
            var randomHydra = Utilities.GetRandomItem(HydraArmy);
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringCollection), randomHydra.StringCollection.ToArray(), ExpressionOperator.NotEqual);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection) == false);
            Assert.DoesNotContain(result, t => t.StringCollection.SequenceEqual(randomHydra.StringCollection));
        }

        /// <summary>
        ///     Asserts that an collection of <see cref="string"/> <see cref="ExpressionOperator.Contains"/> single value expression is generated correctly.
        /// </summary>
        [Fact]
        public void Assert_String_Collection_Contains_Single_Value_Expression_Is_Generated_Correctly()
        {
            // Arrange
            var randomString = Utilities.GetRandomItem(HydraArmy.Select(t => t.StringCollection)).FirstOrDefault();
            var expression = ExpressionBuilder.CreateBinaryExpression<Hydra>(nameof(Hydra.StringCollection), randomString, ExpressionOperator.Contains);

            // Act
            var result = HydraArmy.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.StringCollection.Contains(randomString));
            Assert.DoesNotContain(result, t => t.StringCollection.Contains(randomString) == false);
        }
    }
}
