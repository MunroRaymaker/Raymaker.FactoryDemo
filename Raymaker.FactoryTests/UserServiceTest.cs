using System;
using FluentAssertions;
using NSubstitute;
using Raymaker.FactoryDemo.Console;
using Xunit;

namespace Raymaker.FactoryTests
{
    public class UserServiceTest
    {
        private readonly UserService sut; // System Under Test
        private readonly IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly IUserCreditService userCreditService = Substitute.For<IUserCreditService>();

        public UserServiceTest()
        {
            this.sut = new UserService(
                this.dateTimeProvider, 
                this.userCreditService);
        }

        [Fact]
        public void AddUser_ShouldAddUser_WhenParametersAreValid()
        {
            // Arrange
            var firstName = "Nick";
            var lastName = "Chapsas";
            var dob = new DateTime(1993, 1, 1);

            this.dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 2, 16));
            this.userCreditService.GetCreditLimit(firstName, dob).Returns(600);

            // Act
            var result = sut.AddUser(firstName, 
                lastName, 
                "nick.chapsas@gmail.com", 
                dob,
                "");

            // Assert
            result.Should().BeTrue();
            this.userCreditService.Received(1).GetCreditLimit(Arg.Any<string>(), Arg.Any<DateTime>());
        }

        [Theory]
        [InlineData("","Chapsas", "nick.chapsas@gmail.com", 1993)]
        [InlineData("Nick", "Chapsas", "nickchapsas", 1993)]
        [InlineData("Nick", "Chapsas", "nick.chapsas@gmail.com", 2002)]
        public void AddUser_ShouldNotAddUser_WhenParametersAreInvalid(string firstName, string lastName, string email, int yearOfBirth)
        {
            // Arrange
            var dob = new DateTime(yearOfBirth, 1, 1);

            this.dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 2, 16));
            this.userCreditService.GetCreditLimit(firstName, dob).Returns(600);

            // Act
            var result = sut.AddUser(firstName,
                lastName,
                email,
                dob,
                "");

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("", true, 600, 600)]
        [InlineData("ImportantClient", true, 600, 1200)]
        [InlineData("VeryImportantClient", false, 0, 0)]
        public void AddUser_ShouldCreateUserWithCorrectCreditLimit_WhenNameIndicatesDifferentClassification(string clientName, bool hasCreditLimit, int initialCredit, int finalCreditLimit)
        {
            // Arrange
            var firstName = "Nick";
            var lastName = "Chapsas";
            var dob = new DateTime(1993, 1, 1);

            this.dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 2, 16));
            this.userCreditService.GetCreditLimit(firstName, dob).Returns(initialCredit);

            // Act
            var result = sut.AddUser(firstName,
                lastName,
                "nick.chapsas@gmail.com",
                dob,
                clientName);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void AddUser_ShouldNotCreateUser_WhenCreditLimitIsLessThan500()
        {
            // Arrange
            var firstName = "Nick";
            var lastName = "Chapsas";
            var dob = new DateTime(1993, 1, 1);

            this.dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 2, 16));
            this.userCreditService.GetCreditLimit(firstName, dob).Returns(499);

            // Act
            var result = sut.AddUser(firstName,
                lastName,
                "nick.chapsas@gmail.com",
                dob,
                "");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void AddUser_ShouldNotCreateUser_WhenUserIsUnder21()
        {
            // Arrange
            var firstName = "Nick";
            var lastName = "Chapsas";
            var dob = new DateTime(2000, 2, 15);

            this.dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 2, 16));
            this.userCreditService.GetCreditLimit(firstName, dob).Returns(499);

            // Act
            var result = sut.AddUser(firstName,
                lastName,
                "nick.chapsas@gmail.com",
                dob,
                "");

            // Assert
            result.Should().BeFalse();
        }
    }
}
