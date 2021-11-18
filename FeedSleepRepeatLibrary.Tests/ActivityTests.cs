using System;
using System.Globalization;
using Xunit;

namespace FeedSleepRepeatLibrary.Tests
{
    public class ActivityTests
    {
        private readonly CultureInfo enGB = new("en-GB");

        [Theory]
        [InlineData("06/01/2020 09:00:00", "06/01/2020 09:00:00", 0, 0)]
        [InlineData("10/11/2021 10:14:00", "10/11/2021 10:30:00", 0, 16)]
        [InlineData("01/02/2022 11:20:00", "01/02/2022 12:40:00", 1, 20)]
        [InlineData("28/05/2023 02:30:00", "28/05/2023 19:12:00", 16, 42)]
        [InlineData("24/12/2024 00:00:00", "24/12/2024 23:59:00", 23, 59)]
        public void ActivityLength_TimeDiffShouldBeReturned(string start, string end, int hours, int mins)
        {
            Activity feed = new()
            {
                Start = DateTime.Parse(start, enGB),
                End = DateTime.Parse(end, enGB),
            };

            var expected = (hours, mins);

            var actual = feed.ActivityLength;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("06/01/2020 23:55:00", "07/01/2020 00:10:00", 0, 15)]
        [InlineData("28/05/2021 19:12:00", "29/05/2021 02:30:00", 7, 18)]
        [InlineData("01/02/2022 11:20:00", "02/02/2022 01:00:00", 13, 40)]
        [InlineData("24/12/2023 23:59:00", "25/12/2023 23:58:00", 23, 59)]
        public void ActivityLength_TimeDiffShouldBeReturnedWhenEndInNextDay(string start, string end, int hours, int mins)
        {
            Activity feed = new()
            {
                Start = DateTime.Parse(start, enGB),
                End = DateTime.Parse(end, enGB),
            };

            var expected = (hours, mins);

            var actual = feed.ActivityLength;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(
            "12/10/2021 08:15:00", "12/10/2021 08:30:00", nameof(FeedType.Bottle), "105",
            " Activity Type: Feed     Start: 08:15  End: 08:30     Length: 0h 15m\tFeed Type: Bottle\tAmount: 105ml")]
        [InlineData(
            "29/02/2024 21:00:00", "29/02/2024 21:25:00", nameof(FeedType.Breast), "60",
            " Activity Type: Feed     Start: 21:00  End: 21:25     Length: 0h 25m\tFeed Type: Breast\tAmount: 60ml")]
        [InlineData(
            "01/01/2020 22:47:00", "01/02/2020 00:05:00", nameof(FeedType.Solid), "200",
            " Activity Type: Feed     Start: 22:47  End: 00:05     Length: 1h 18m\tFeed Type: Solid\tAmount: 200g")]
        public void ActivitySummary_FeedDetailsShouldBeSummarised(string start, string end, string feedType, string feedAmount, string expected)
        {
            var bottleFeedActivity = new Activity
            {
                ActivityType = ActivityType.Feed,
                Start = DateTime.Parse(start, enGB),
                End = DateTime.Parse(end, enGB),
                FeedType = feedType,
                FeedAmount = feedAmount,
            };

            var actual = bottleFeedActivity.ActivitySummary;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ActivitySummary_SleepDetailsShouldBeSummarised()
        {
            var sleepActivity = new Activity
            {
                ActivityType = ActivityType.Sleep,
                Start = new DateTime(2022, 7, 30, 15, 20, 0),
                End = new DateTime(2022, 7, 30, 16, 50, 0),
                SleepPlace = "Cot",
            };

            var expected = " Activity Type: Sleep    Start: 15:20  End: 16:50     Length: 1h 30m\tPlace: Cot";

            var actual = sleepActivity.ActivitySummary;

            Assert.Equal(expected, actual);
        }
    }
}
