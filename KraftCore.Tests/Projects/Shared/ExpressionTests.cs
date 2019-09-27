namespace KraftCore.Tests.Projects.Shared
{
    using System;
    using System.Linq;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Utilities;
    using Xunit;

    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Tests for the expression builder/extensions.
    /// </summary>
    public class ExpressionTests
    {
        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Equal"/> comparison gives the correct result when operating on numeric values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Equality_Comparison_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.Age), randomPerson.Age, ExpressionOperator.Equal);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age == randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age != randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.NotEqual"/> comparison gives the correct result when operating on numeric values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Non_Equality_Comparison_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.Age), randomPerson.Age, ExpressionOperator.NotEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age != randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age == randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.LessThan"/> comparison gives the correct result when operating on numeric values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Less_Than_Comparison_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.Age), randomPerson.Age, ExpressionOperator.LessThan);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age < randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age >= randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.LessThanOrEqual"/> comparison gives the correct result when operating on numeric values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Less_Than_Or_Equal_Comparison_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.Age), randomPerson.Age, ExpressionOperator.LessThanOrEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age <= randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age > randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.GreaterThan"/> comparison gives the correct result when operating on numeric values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Greater_Than_Comparison_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.Age), randomPerson.Age, ExpressionOperator.GreaterThan);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age > randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age <= randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.GreaterThanOrEqual"/> comparison gives the correct result when operating on numeric values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Greater_Than_Or_Equal_Comparison_Of_Numeric_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.Age), randomPerson.Age, ExpressionOperator.GreaterThanOrEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age >= randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age < randomPerson.Age);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Equal"/> comparison gives the correct result when operating on DateTime values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Equality_Comparison_Of_DateTime_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.DateOfBirth), randomPerson.DateOfBirth, ExpressionOperator.Equal);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth == randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth != randomPerson.DateOfBirth);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.NotEqual"/> comparison gives the correct result when operating on DateTime values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Non_Equality_Comparison_Of_DateTime_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.DateOfBirth), randomPerson.DateOfBirth, ExpressionOperator.NotEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth != randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth == randomPerson.DateOfBirth);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.LessThan"/> comparison gives the correct result when operating on DateTime values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Less_Than_Comparison_Of_DateTime_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.DateOfBirth), randomPerson.DateOfBirth, ExpressionOperator.LessThan);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth < randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth >= randomPerson.DateOfBirth);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.LessThanOrEqual"/> comparison gives the correct result when operating on DateTime values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Less_Than_Or_Equal_Comparison_Of_DateTime_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.DateOfBirth), randomPerson.DateOfBirth, ExpressionOperator.LessThanOrEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth <= randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth > randomPerson.DateOfBirth);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.GreaterThan"/> comparison gives the correct result when operating on DateTime values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Greater_Than_Comparison_Of_DateTime_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.DateOfBirth), randomPerson.DateOfBirth, ExpressionOperator.GreaterThan);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth > randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth <= randomPerson.DateOfBirth);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.GreaterThanOrEqual"/> comparison gives the correct result when operating on DateTime values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Greater_Than_Or_Equal_Comparison_Of_DateTime_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.DateOfBirth), randomPerson.DateOfBirth, ExpressionOperator.GreaterThanOrEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth >= randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth < randomPerson.DateOfBirth);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Equal"/> comparison gives the correct result when operating on string values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Equality_Comparison_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FirstName), randomPerson.FirstName, ExpressionOperator.Equal);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FirstName == randomPerson.FirstName);
            Assert.DoesNotContain(result, t => t.FirstName != randomPerson.FirstName);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.NotEqual"/> comparison gives the correct result when operating on string values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Non_Equality_Comparison_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FirstName), randomPerson.FirstName, ExpressionOperator.NotEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FirstName != randomPerson.FirstName);
            Assert.DoesNotContain(result, t => t.FirstName == randomPerson.FirstName);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Contains"/> comparison gives the correct result when operating on string values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FullName), randomPerson.FirstName, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.Contains(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.Contains(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.StartsWith"/> comparison gives the correct result when operating on string values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Starts_With_Comparison_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FullName), randomPerson.FirstName, ExpressionOperator.StartsWith);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.StartsWith(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.StartsWith(randomPerson.FirstName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.EndsWith"/> comparison gives the correct result when operating on string values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Ends_With_Comparison_Of_String_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FullName), randomPerson.LastName, ExpressionOperator.EndsWith);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FullName.EndsWith(randomPerson.LastName, StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(result, t => t.FullName.EndsWith(randomPerson.LastName, StringComparison.OrdinalIgnoreCase) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Equal"/> comparison gives the correct result when operating on non-specific/complex <see cref="object"/> values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Equality_Comparison_Of_Object_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.BestFriend), randomPerson.BestFriend, ExpressionOperator.Equal);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.BestFriend == randomPerson.BestFriend);
            Assert.DoesNotContain(result, t => t.BestFriend != randomPerson.BestFriend);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.NotEqual"/> comparison gives the correct result when operating on non-specific/complex <see cref="object"/> values.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Non_Equality_Comparison_Of_Object_Values_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.BestFriend), randomPerson.BestFriend, ExpressionOperator.NotEqual);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.BestFriend != randomPerson.BestFriend);
            Assert.DoesNotContain(result, t => t.BestFriend == randomPerson.BestFriend);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Contains"/> comparison gives the correct result when operating on generic collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_Generic_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var randomFavNumber = Utilities.GetRandomItem(randomPerson.FavoriteNumbers);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteNumbers), randomFavNumber, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteNumbers.Contains(randomFavNumber));
            Assert.DoesNotContain(result, t => t.FavoriteNumbers.Contains(randomFavNumber) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Contains"/> comparison gives the correct result when operating on generic string collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_Generic_String_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var randomFavWord = Utilities.GetRandomItem(randomPerson.FavoriteWords);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteWords), randomFavWord, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteWords.Contains(randomFavWord));
            Assert.DoesNotContain(result, t => t.FavoriteWords.Contains(randomFavWord) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Contains"/> comparison gives the correct result when operating on non-generic collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_Non_Generic_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var randomFavFruit = Utilities.GetRandomItem(randomPerson.FavoriteFruits.ToArray());
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteFruits), randomFavFruit, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteFruits.Contains(randomFavFruit));
            Assert.DoesNotContain(result, t => t.FavoriteFruits.Contains(randomFavFruit) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.Contains"/> comparison gives the correct result when operating on array collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_Array_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomPerson = Utilities.GetRandomItem(persons);
            var randomFavColor = Utilities.GetRandomItem(randomPerson.FavoriteColors);
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteColors), randomFavColor, ExpressionOperator.Contains);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteColors.Contains(randomFavColor));
            Assert.DoesNotContain(result, t => t.FavoriteColors.Contains(randomFavColor) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.ContainsOnValue"/> comparison gives the correct result when operating on generic collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_On_Value_Comparison_Of_Generic_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomFavNumbers = Utilities.GetRandomItems(persons.Select(t => t.FavoriteNumbers));
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteNumbers), randomFavNumbers, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomFavNumbers.Contains(t.FavoriteNumbers));
            Assert.DoesNotContain(result, t => randomFavNumbers.Contains(t.FavoriteNumbers) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.ContainsOnValue"/> comparison gives the correct result when operating on generic string collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_On_Value_Comparison_Of_Generic_String_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomNames = Utilities.GetRandomItems(persons.Select(t => t.FullName));
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FullName), randomNames, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomNames.Contains(t.FullName));
            Assert.DoesNotContain(result, t => randomNames.Contains(t.FullName) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of <see cref="ExpressionOperator.ContainsOnValue"/> comparison gives the correct result when operating on non-generic collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_On_Value_Comparison_Of_Non_Generic_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomFavFruits = Utilities.GetRandomItems(persons.Select(t => t.FavoriteFruits));
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteFruits), randomFavFruits, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomFavFruits.Contains(t.FavoriteFruits));
            Assert.DoesNotContain(result, t => randomFavFruits.Contains(t.FavoriteFruits) == false);
        }

        /// <summary>
        /// Asserts that an binary expression of '<see cref="ExpressionOperator.ContainsOnValue"/>' comparison gives the correct result when operating on array collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_On_Value_Comparison_Of_Array_Collection_Gives_Correct_Result()
        {
            // Arrange
            var persons = Utilities.GetFakePersonCollection();
            var randomFavColors = Utilities.GetRandomItems(persons.Select(t => t.FavoriteColors));
            var expression = ExpressionBuilder.CreateBinaryExpression<Person>(nameof(Person.FavoriteColors), randomFavColors, ExpressionOperator.ContainsOnValue);

            // Act
            var result = persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomFavColors.Contains(t.FavoriteColors));
            Assert.DoesNotContain(result, t => randomFavColors.Contains(t.FavoriteColors) == false);
        }
    }
}