namespace KraftCore.Tests.Projects.Shared.DynamicQueryBuilder
{
    using System.Linq;
    using KraftCore.Shared.DynamicQuery;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Projects.Shared.DynamicQueryBuilder.Base;
    using KraftCore.Tests.Utilities;
    using Xunit;

    // ReSharper disable InconsistentNaming

    /// <summary>
    ///     Tests for the dynamic query builder.
    /// </summary>
    public class DynamicQueryBuilderNumericTests : DynamicQueryBuilderTestsBase
    {
        /// <summary>
        ///     Asserts that an equality query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.Age), randomPerson.Age));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age == randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age != randomPerson.Age);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.Age), randomPerson.Age));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age != randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age == randomPerson.Age);
        }

        /// <summary>
        ///     Asserts that an less than query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThan, nameof(Person.Age), randomPerson.Age));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age < randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age >= randomPerson.Age);
        }

        /// <summary>
        ///     Asserts that an less than or equal query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Or_Equal_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThanOrEqual, nameof(Person.Age), randomPerson.Age));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age <= randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age > randomPerson.Age);
        }

        /// <summary>
        ///     Asserts that an greater than query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThan, nameof(Person.Age), randomPerson.Age));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age > randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age <= randomPerson.Age);
        }

        /// <summary>
        ///     Asserts that an greater than or equal query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Or_Equal_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThanOrEqual, nameof(Person.Age), randomPerson.Age));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.Age >= randomPerson.Age);
            Assert.DoesNotContain(result, t => t.Age < randomPerson.Age);
        }

        /// <summary>
        ///     Asserts that an binary expression of <see cref="ExpressionOperator.Contains" /> comparison gives the correct result
        ///     when operating on generic collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_Generic_Collection_Of_Numbers_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var randomFavNumber = Utilities.GetRandomItem(randomPerson.FavoriteNumbers);
            var expression = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Contains, nameof(Person.FavoriteNumbers), randomFavNumber));

            // Act
            var result = Persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.FavoriteNumbers.Contains(randomFavNumber));
            Assert.DoesNotContain(result, t => t.FavoriteNumbers.Contains(randomFavNumber) == false);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomAge = Utilities.GetRandomItems(Persons.Select(t => t.Age));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Person.Age), string.Join(',', randomAge.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomAge.Contains(t.Age));
            Assert.DoesNotContain(result, t => randomAge.Contains(t.Age) == false);
        }

        /// <summary>
        ///     Asserts that an equality query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.AccountBalance), randomPerson.AccountBalance));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.AccountBalance == randomPerson.AccountBalance);
            Assert.DoesNotContain(result, t => t.AccountBalance != randomPerson.AccountBalance);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.AccountBalance), randomPerson.AccountBalance));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.AccountBalance != randomPerson.AccountBalance);
            Assert.DoesNotContain(result, t => t.AccountBalance == randomPerson.AccountBalance);
        }

        /// <summary>
        ///     Asserts that an less than query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThan, nameof(Person.AccountBalance), randomPerson.AccountBalance));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.AccountBalance < randomPerson.AccountBalance);
            Assert.DoesNotContain(result, t => t.AccountBalance >= randomPerson.AccountBalance);
        }

        /// <summary>
        ///     Asserts that an less than or equal query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Or_Equal_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThanOrEqual, nameof(Person.AccountBalance), randomPerson.AccountBalance));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.AccountBalance <= randomPerson.AccountBalance);
            Assert.DoesNotContain(result, t => t.AccountBalance > randomPerson.AccountBalance);
        }

        /// <summary>
        ///     Asserts that an greater than query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThan, nameof(Person.AccountBalance), randomPerson.AccountBalance));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.AccountBalance > randomPerson.AccountBalance);
            Assert.DoesNotContain(result, t => t.AccountBalance <= randomPerson.AccountBalance);
        }

        /// <summary>
        ///     Asserts that an greater than or equal query with single element of numeric type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Or_Equal_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThanOrEqual, nameof(Person.AccountBalance), randomPerson.AccountBalance));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.AccountBalance >= randomPerson.AccountBalance);
            Assert.DoesNotContain(result, t => t.AccountBalance < randomPerson.AccountBalance);
        }

        /// <summary>
        ///     Asserts that an binary expression of <see cref="ExpressionOperator.Contains" /> comparison gives the correct result
        ///     when operating on generic collections.
        /// </summary>
        [Fact]
        public void Assert_Binary_Expression_Of_Contains_Comparison_Of_Generic_Collection_Of_Nullable_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var randomFavYear = Utilities.GetRandomItem(randomPerson.LeastFavoriteNumbers);
            var expression = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Contains, nameof(Person.LeastFavoriteNumbers), randomFavYear));

            // Act
            var result = Persons.Where(expression.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.LeastFavoriteNumbers.Contains(randomFavYear));
            Assert.DoesNotContain(result, t => t.LeastFavoriteNumbers.Contains(randomFavYear) == false);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Nullable_Numeric_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomAccountBalance = Utilities.GetRandomItems(Persons.Select(t => t.AccountBalance));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue,
                                                                         nameof(Person.AccountBalance),
                                                                         string.Join(',', randomAccountBalance.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomAccountBalance.Contains(t.AccountBalance));
            Assert.DoesNotContain(result, t => randomAccountBalance.Contains(t.AccountBalance) == false);
        }
    }
}