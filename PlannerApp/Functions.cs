using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp
{

    /// <summary>
    /// Klasa <c>Functions</c>
    /// Zawiera pomocnicze funckje związane z wyświetlaniem wydarzeń 
    /// </summary>
    /// <list>
    /// <item>
    ///     <term>FirstDayOfWeek</term>
    ///     <description>Obliczanie daty pierwszego dnia tygodnia</description>
    /// </item>
    /// <item>
    ///     <term>HourToRow</term>
    ///     <description>Obliczanie rzędu w siatce kalendarza</description>
    /// </item>
    /// <item>
    ///     <term>ColumnToRow</term>
    ///     <description>Obliczanie kolumny rzędu w siatce kalendarza</description>
    /// </item>
    /// </list>
    public static class Functions
    {

        /// <summary>
        /// Oblicza datę poniedziałku aktualnego tygodnia
        /// </summary>
        /// <param name="dt">Data</param>
        /// <returns>Zwraca datę poniedziałku w aktualnym tygodniu</returns>
        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-diff).Date;
        }

        /// <summary>
        /// Zamienia godzinę rozpoczęcie wydarzenia na rząd w siatce kalendarza, w którym ma być wyświetlane wydarzenie
        /// </summary>
        /// <param name="hour">Godzina rozpoczęcia wydarzenia</param>
        /// <returns></returns>
        /// <seealso cref="Functions.DayToColumn(DateTime)"/>
        public static int HourToRow(int hour)
        {
            if (hour > 18 || hour < 7)
                return -1;
            else
                return hour - 7;
        }

        /// <summary>
        /// Zamienia dzień tygodnia wydarzenia na kolumnę w siatce kalendarza, w którym ma być wyświetlane wydarzenie
        /// </summary>
        /// <param name="date">Dzień tygodnia wydarzenia</param>
        /// <returns>Zwraca numer kolumny, w której powinno być wyświetlane wydarzenie</returns>
        /// <seealso cref="Functions.HourToRow(int)"/>
        public static int DayToColumn(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            if (day > 0)
            {
                return day - 1;
            }
            else
            {
                return 6;
            }
        }
    }
}
