namespace KraftCore.Tests.Projects.Utils
{
    using System;
    using System.Linq;
    using KraftCore.Utils.Expressions;
    using Xunit;

    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Tests for the expression builder/extensions.
    /// </summary>
    public class ExpressionTests
    {
        /// <summary>
        /// Asserts that an numeric equality binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Equality_Binary_Expression_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.Age), randomPerson.Age, ExpressionOperator.Equal);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age == randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age != randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an numeric non-equality binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Binary_Expression_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.Age), randomPerson.Age, ExpressionOperator.NotEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age != randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age == randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an numeric less than binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Binary_Expression_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.Age), randomPerson.Age, ExpressionOperator.LessThan);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age < randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age >= randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an numeric less than or equal binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Or_Equal_Binary_Expression_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.Age), randomPerson.Age, ExpressionOperator.LessThanOrEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age <= randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age > randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an numeric greater than binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Binary_Expression_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.Age), randomPerson.Age, ExpressionOperator.GreaterThan);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age > randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age <= randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an numeric greater than or equal binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Or_Equal_Binary_Expression_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.Age), randomPerson.Age, ExpressionOperator.GreaterThanOrEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age >= randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age < randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an string equality binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Equality_Binary_Expression_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FirstName), randomPerson.FirstName, ExpressionOperator.Equal);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FirstName == randomPerson.FirstName);
            Assert.DoesNotContain(result, t => t.FirstName != randomPerson.FirstName);
        }

        /// <summary>
        /// Asserts that an string non-equality binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Binary_Expression_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FirstName), randomPerson.FirstName, ExpressionOperator.NotEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FirstName != randomPerson.FirstName);
            Assert.DoesNotContain(result, t => t.FirstName == randomPerson.FirstName);
        }

        /// <summary>
        /// Asserts that an string contains binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_Binary_Expression_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FullName), randomPerson.FirstName, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.Contains(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.Contains(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an string starts with binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Starts_With_Binary_Expression_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FullName), randomPerson.FirstName, ExpressionOperator.StartsWith);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.StartsWith(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.StartsWith(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an string ends with binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Ends_With_Binary_Expression_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FullName), randomPerson.LastName, ExpressionOperator.EndsWith);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.EndsWith(randomPerson.LastName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.EndsWith(randomPerson.LastName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an generic collection contains binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_Binary_Expression_Of_Generic_Collection_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var randomFavNumber = TestUtilities.GetRandomItem(randomPerson.FavoriteNumbers);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FavoriteNumbers), randomFavNumber, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteNumbers.Contains(randomFavNumber));
            Assert.DoesNotContain(result, t => t.FavoriteNumbers.Contains(randomFavNumber) == false);
        }

        /// <summary>
        /// Asserts that an non-generic collection contains binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_Binary_Expression_Of_Non_Generic_Collection_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var randomFavFruit = TestUtilities.GetRandomItem(randomPerson.FavoriteFruits.ToArray());
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FavoriteFruits), randomFavFruit, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteFruits.Contains(randomFavFruit));
            Assert.DoesNotContain(result, t => t.FavoriteFruits.Contains(randomFavFruit) == false);
        }

        /// <summary>
        /// Asserts that an array contains binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_Binary_Expression_Of_Array_Collection_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var randomPerson = TestUtilities.GetRandomItem(persons);
            var randomFavColor = TestUtilities.GetRandomItem(randomPerson.FavoriteColors);
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FavoriteColors), randomFavColor, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteColors.Contains(randomFavColor));
            Assert.DoesNotContain(result, t => t.FavoriteColors.Contains(randomFavColor) == false);
        }

        /// <summary>
        /// Asserts that an generic collection contains on value binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Binary_Expression_Of_Generic_Collection_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var favNumbers = TestUtilities.GetRandomItems(persons.Select(t => t.FavoriteNumbers));
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FavoriteNumbers), favNumbers, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => favNumbers.Contains(t.FavoriteNumbers));
            Assert.DoesNotContain(result, t => favNumbers.Contains(t.FavoriteNumbers) == false);
        }

        /// <summary>
        /// Asserts that an non-generic collection contains on value binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Binary_Expression_Of_Non_Generic_Collection_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var favFruits = TestUtilities.GetRandomItems(persons.Select(t => t.FavoriteFruits));
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FavoriteFruits), favFruits, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => favFruits.Contains(t.FavoriteFruits));
            Assert.DoesNotContain(result, t => favFruits.Contains(t.FavoriteFruits) == false);
        }

        /// <summary>
        /// Asserts that an array contains on value binary expression gives the correct result.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Binary_Expression_Of_Array_Collection_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = TestUtilities.GetFakePersonCollection();
            var favColors = TestUtilities.GetRandomItems(persons.Select(t => t.FavoriteColors));
            var expression = ExpressionBuilder.CreateBinaryExpression<TestUtilities.Person>(nameof(TestUtilities.Person.FavoriteColors), favColors, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => favColors.Contains(t.FavoriteColors));
            Assert.DoesNotContain(result, t => favColors.Contains(t.FavoriteColors) == false);
        }
    }
}
