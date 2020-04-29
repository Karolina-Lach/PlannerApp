using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PlannerApp.Tests
{
    class FunctionsTests
    {
        [Test]
        public void FirstDayOfTheWeek_Test()
        {
            DateTime result = Functions.FirstDayOfWeek(new DateTime(2020, 4, 20));
            var correctResult = new DateTime(2020, 4, 20);

            Assert.AreEqual(result.Date, correctResult.Date);
        }

        [TestCase(2020, 4, 20, 2020, 4, 20)]
        [TestCase(2020, 4, 21, 2020, 4, 20)]
        [TestCase(2020, 4, 22, 2020, 4, 20)]
        [TestCase(2020, 4, 23, 2020, 4, 20)]
        [TestCase(2020, 4, 24, 2020, 4, 20)]
        [TestCase(2020, 4, 25, 2020, 4, 20)]
        [TestCase(2020, 4, 26, 2020, 4, 20)]
        public void FirstDayOfTheWeek_TestCase(int year, int month, int day, int yearRes, int monthRes, int dayRes)
        {
            DateTime result = Functions.FirstDayOfWeek(new DateTime(year, month, day));
            var correctResult = new DateTime(yearRes, monthRes, dayRes);

            Assert.AreEqual(result.Date, correctResult.Date);
        }

        [TestCase(2020, 4, 20, 0)]
        [TestCase(2020, 4, 21, 1)]
        [TestCase(2020, 4, 22, 2)]
        [TestCase(2020, 4, 23, 3)]
        [TestCase(2020, 4, 24, 4)]
        [TestCase(2020, 4, 25, 5)]
        [TestCase(2020, 4, 26, 6)]
        public void DayToColumn_Test(int year, int month, int day, int result)
        {
            int column = Functions.DayToColumn(new DateTime(year, month, day));

            Assert.AreEqual(column, result);
        }

        [TestCase(24, -1)]
        [TestCase(6, -1)]
        [TestCase(7, 0)]
        [TestCase(18, 11)]
        [TestCase(10, 3)]
        public void HourToRow_Test(int hour, int result)
        {
            int row = Functions.HourToRow(hour);

            Assert.AreEqual(result, row);
        }
    }
}
