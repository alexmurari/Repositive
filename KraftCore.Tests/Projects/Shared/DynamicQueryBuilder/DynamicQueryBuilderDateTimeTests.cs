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
    public class DynamicQueryBuilderDateTimeTests : DynamicQueryBuilderTestsBase
    {
        /// <summary>
        ///     Asserts that an equality query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal, nameof(Person.DateOfBirth), randomPerson.DateOfBirth.ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth == randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth != randomPerson.DateOfBirth);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual, nameof(Person.DateOfBirth), randomPerson.DateOfBirth.ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth != randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth == randomPerson.DateOfBirth);
        }

        /// <summary>
        ///     Asserts that an less than query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThan, nameof(Person.DateOfBirth), randomPerson.DateOfBirth.ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth < randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth >= randomPerson.DateOfBirth);
        }

        /// <summary>
        ///     Asserts that an less than or equal query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Or_Equal_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThanOrEqual, nameof(Person.DateOfBirth), randomPerson.DateOfBirth.ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth <= randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth > randomPerson.DateOfBirth);
        }

        /// <summary>
        ///     Asserts that an greater than query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThan, nameof(Person.DateOfBirth), randomPerson.DateOfBirth.ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth > randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth <= randomPerson.DateOfBirth);
        }

        /// <summary>
        ///     Asserts that an greater than or equal query with single element of DateTime type gives correct result when
        ///     executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Or_Equal_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThanOrEqual, nameof(Person.DateOfBirth), randomPerson.DateOfBirth.ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfBirth >= randomPerson.DateOfBirth);
            Assert.DoesNotContain(result, t => t.DateOfBirth < randomPerson.DateOfBirth);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomAge = Utilities.GetRandomItems(Persons.Select(t => t.DateOfBirth));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue, nameof(Person.DateOfBirth), string.Join(',', randomAge.Select(x => $"'{x:O}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomAge.Contains(t.DateOfBirth));
            Assert.DoesNotContain(result, t => randomAge.Contains(t.DateOfBirth) == false);
        }

        /// <summary>
        ///     Asserts that an equality query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Equality_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.Equal,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         randomPerson.DateOfDriversLicense.GetValueOrDefault().ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfDriversLicense == randomPerson.DateOfDriversLicense);
            Assert.DoesNotContain(result, t => t.DateOfDriversLicense != randomPerson.DateOfDriversLicense);
        }

        /// <summary>
        ///     Asserts that an non-equality query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Non_Equality_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.NotEqual,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         randomPerson.DateOfDriversLicense.GetValueOrDefault().ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfDriversLicense != randomPerson.DateOfDriversLicense);
            Assert.DoesNotContain(result, t => t.DateOfDriversLicense == randomPerson.DateOfDriversLicense);
        }

        /// <summary>
        ///     Asserts that an less than query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThan,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         randomPerson.DateOfDriversLicense.GetValueOrDefault().ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfDriversLicense < randomPerson.DateOfDriversLicense);
            Assert.DoesNotContain(result, t => t.DateOfDriversLicense >= randomPerson.DateOfDriversLicense);
        }

        /// <summary>
        ///     Asserts that an less than or equal query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Less_Than_Or_Equal_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.LessThanOrEqual,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         randomPerson.DateOfDriversLicense.GetValueOrDefault().ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfDriversLicense <= randomPerson.DateOfDriversLicense);
            Assert.DoesNotContain(result, t => t.DateOfDriversLicense > randomPerson.DateOfDriversLicense);
        }

        /// <summary>
        ///     Asserts that an greater than query with single element of DateTime type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThan,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         randomPerson.DateOfDriversLicense.GetValueOrDefault().ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfDriversLicense > randomPerson.DateOfDriversLicense);
            Assert.DoesNotContain(result, t => t.DateOfDriversLicense <= randomPerson.DateOfDriversLicense);
        }

        /// <summary>
        ///     Asserts that an greater than or equal query with single element of DateTime type gives correct result when
        ///     executed.
        /// </summary>
        [Fact]
        public void Assert_Greater_Than_Or_Equal_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomPerson = Utilities.GetRandomItem(Persons);
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.GreaterThanOrEqual,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         randomPerson.DateOfDriversLicense.GetValueOrDefault().ToString("O")));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => t.DateOfDriversLicense >= randomPerson.DateOfDriversLicense);
            Assert.DoesNotContain(result, t => t.DateOfDriversLicense < randomPerson.DateOfDriversLicense);
        }

        /// <summary>
        ///     Asserts that an contains on value query with single element of string type gives correct result when executed.
        /// </summary>
        [Fact]
        public void Assert_Contains_On_Value_Query_With_Single_Element_Of_Nullable_DateTime_Type_Gives_Correct_Result()
        {
            // Arrange
            var randomAge = Utilities.GetRandomItems(Persons.Select(t => t.DateOfDriversLicense));
            var query = DynamicQueryBuilder.Build<Person>(BuildQueryText(ExpressionOperator.ContainsOnValue,
                                                                         nameof(Person.DateOfDriversLicense),
                                                                         string.Join(',', randomAge.Select(x => $"'{x:O}'"))));

            // Act
            var result = Persons.Where(query.Compile()).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, t => randomAge.Contains(t.DateOfDriversLicense));
            Assert.DoesNotContain(result, t => randomAge.Contains(t.DateOfDriversLicense) == false);
        }
    }
}