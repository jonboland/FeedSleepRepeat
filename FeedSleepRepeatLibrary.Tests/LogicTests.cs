using System;
using Xunit;

namespace FeedSleepRepeatLibrary.Tests
{
    public class LogicTests
    {
        [Theory]
        [InlineData(0, 0, "0")]
        [InlineData(5, 3, "8")]
        [InlineData(14, 11, "25")]
        [InlineData(100, 100, "200")]
        public void RefreshTotalNappies_ValuesShouldTotal(decimal x, decimal y, string expected)
        {
            string actual = FeedSleepRepeatLogic.RefreshTotalNappies(x, y);

            Assert.Equal(expected, actual);
        }
    }
}

