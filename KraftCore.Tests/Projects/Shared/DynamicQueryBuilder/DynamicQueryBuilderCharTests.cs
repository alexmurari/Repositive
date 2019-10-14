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
    public class DynamicQueryBuilderCharTests : DynamicQueryBuilderTestsBase
    {
        /// <summary>
        ///     Asserts that an equality query with single element of Char type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Char_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.PersonChar), randomPerson.PersonChar));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.PersonChar == randomPerson.PersonChar);
            Assert.DoesNotContain(result, t => t.PersonChar != randomPerson.PersonChar);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of Char type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Char_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.PersonChar), randomPerson.PersonChar));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.PersonChar != randomPerson.PersonChar);
            Assert.DoesNotContain(result, t => t.PersonChar == randomPerson.PersonChar);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of Char type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Char_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPersonChar = Utilities.GetRandomItems(Persons.Select(t => t.PersonChar));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Person.PersonChar), string.Join(',', randomPersonChar.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomPersonChar.Contains(t.PersonChar));
            Assert.DoesNotContain(result, t => randomPersonChar.Contains(t.PersonChar) == false);
        }

        /// <summary>
        ///     Asserts that an equality query with single element of Char type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Nullable_Char_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.OptionalPersonChar), randomPerson.OptionalPersonChar));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.OptionalPersonChar == randomPerson.OptionalPersonChar);
            Assert.DoesNotContain(result, t => t.OptionalPersonChar != randomPerson.OptionalPersonChar);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of Char type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Nullable_Char_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.OptionalPersonChar), randomPerson.OptionalPersonChar));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.OptionalPersonChar != randomPerson.OptionalPersonChar);
            Assert.DoesNotContain(result, t => t.OptionalPersonChar == randomPerson.OptionalPersonChar);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of Char type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Nullable_Char_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPersonChar = Utilities.GetRandomItems(Persons.Select(t => t.OptionalPersonChar));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Person.OptionalPersonChar), string.Join(',', randomPersonChar.Select(x => $"'{x}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomPersonChar.Contains(t.OptionalPersonChar));
            Assert.DoesNotContain(result, t => randomPersonChar.Contains(t.OptionalPersonChar) == false);
        }
    }
}