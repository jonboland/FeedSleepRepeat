using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Globalization;

namespace FeedSleepRepeatLibrary.Tests
{
    public class LogicTests
    {
        [Fact]
        public void AssembleFeedTypes_ListShouldContainEmptyStringAndEnumNames()
        {
            var expected = new List<string> 
            { 
                string.Empty, 
                nameof(FeedType.Bottle), 
                nameof(FeedType.Breast), 
                nameof(FeedType.Solid),
            };

            var actual = FeedSleepRepeatLogic.AssembleFeedTypes();

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("15/10/2021", "15/10/2021", "0y 0m 0d")]
        [InlineData("09/09/2021", "22/09/2021", "0y 0m 13d")]
        [InlineData("28/05/2019", "28/06/2019", "0y 1m 0d")]
        [InlineData("02/02/2024", "13/03/2024", "0y 1m 9d")]
        [InlineData("10/12/2021", "15/01/2023", "1y 1m 5d")]
        public void CalculateAge_DateOfBirthShouldConvertToAge(string dob, string today, string expected)
        {
            var dateOfBirth = DateTime.Parse(dob, new CultureInfo("en-GB"));

            string actual = FeedSleepRepeatLogic.CalculateAge(dateOfBirth, today);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, "0")]
        [InlineData(5, 3, "8")]
        [InlineData(14, 11, "25")]
        [InlineData(100, 100, "200")]
        public void RefreshTotalNappies_DecimalsShouldTotalAsString(decimal x, decimal y, string expected)
        {
            string actual = FeedSleepRepeatLogic.RefreshTotalNappies(x, y);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateActivityInstance_SecondsShouldBeTruncated()
        {
            var original = new DateTime(2021, 10, 8, 7, 14, 50);
            var expected = new DateTime(2021, 10, 8, 7, 14, 0);

            Activity feed = FeedSleepRepeatLogic.GenerateActivityInstance(
                ActivityType.Feed, 0, feedStart: original, 
                feedEnd: default, feedAmount: default, feedType: default);

            var actual = feed.Start;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateActivityInstance_SecondsAndMillisecondsShouldBeTruncated()
        {
            var original = new DateTime(2023, 2, 14, 17, 3, 50, 125);
            var expected = new DateTime(2023, 2, 14, 17, 3, 0, 0);

            Activity sleep = FeedSleepRepeatLogic.GenerateActivityInstance(
                ActivityType.Sleep, 1, sleepStart: default,
                sleepEnd: original, sleepPlace: default);

            var actual = sleep.End;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateActivityInstance_SecondsMillisecondsAndTicksShouldBeTruncated()
        {
            var original = new DateTime(2019, 4, 28, 11, 9, 6, 139).AddTicks(5050);
            var expected = new DateTime(2019, 4, 28, 11, 9, 0, 0);

            Activity feed = FeedSleepRepeatLogic.GenerateActivityInstance(
                ActivityType.Feed, 101, feedStart: default,
                feedEnd: original, feedAmount: default, feedType: default);

            var actual = feed.End;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void SortActivities_ActivityListShouldBeInStartTimeOrder()
        {
            Activity activity = new Activity
            {
                ActivityType = ActivityType.Feed,
                Start = new DateTime(2021, 10, 8, 7, 14, 0),
                End = new DateTime(2021, 10, 8, 7, 20, 0),
                FeedType = nameof(FeedType.Bottle),
                FeedAmount = "105",
            };

            Activity activity2 = new Activity
            {
                ActivityType = ActivityType.Feed,
                Start = new DateTime(2021, 10, 8, 5, 0, 0),
                End = new DateTime(2021, 10, 8, 5, 20, 0),
                FeedType = nameof(FeedType.Breast),
                FeedAmount = "60",
            };

            Activity activity3 = new Activity
            {
                ActivityType = ActivityType.Feed,
                Start = new DateTime(2021, 10, 8, 6, 0, 0),
                End = new DateTime(2021, 10, 8, 6, 13, 0),
                FeedType = nameof(FeedType.Bottle),
                FeedAmount = "120",
            };

            Activity activity4 = new Activity
            {
                ActivityType = ActivityType.Sleep,
                Start = new DateTime(2021, 10, 8, 5, 30, 0),
                End = new DateTime(2021, 10, 8, 6, 0, 0),
                SleepPlace = "Cot",
            };

            BabyDay expected = new();
            BabyDay actual = new();

            expected.Activities.AddRange(new List<Activity> { activity2, activity4, activity3, activity });

            actual.Activities.AddRange(new List<Activity> { activity, activity2, activity3, activity4 });

            actual.Activities = FeedSleepRepeatLogic.SortActivities(actual.Activities);

            actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
        }
    }
}
