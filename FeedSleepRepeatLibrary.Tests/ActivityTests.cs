using System;
using Xunit;

namespace FeedSleepRepeatLibrary.Tests
{
    public class ActivityTests
    {
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
                Start = DateTime.Parse(start),
                End = DateTime.Parse(end),
            };

            var expected = (hours, mins);

            var actual = feed.ActivityLength;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("06/01/2020 23:55:00", "06/01/2020 00:10:00", 0, 15)]
        [InlineData("28/05/2021 19:12:00", "28/05/2021 02:30:00", 7, 18)]
        [InlineData("01/02/2022 11:20:00", "01/02/2022 01:00:00", 13, 40)]
        [InlineData("24/12/2023 23:59:00", "24/12/2023 23:58:00", 23, 59)]
        public void ActivityLength_PositiveTimeDiffShouldBeReturnedWhenEndBeforeStart(string start, string end, int hours, int mins)
        {
            Activity feed = new()
            {
                Start = DateTime.Parse(start),
                End = DateTime.Parse(end),
            };

            var expected = (hours, mins);

            var actual = feed.ActivityLength;

            Assert.Equal(expected, actual);
        }
    }
}
