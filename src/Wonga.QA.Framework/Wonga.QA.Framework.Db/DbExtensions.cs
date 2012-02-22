﻿using System;
using System.Data.Linq;
using System.Linq;
using Wonga.QA.Framework.Core;

namespace Wonga.QA.Framework.Db
{
    public static class DbExtensions
    {
        public static T Insert<T>(this Table<T> table, T entity) where T : DbEntity<T>
        {
            table.InsertOnSubmit(entity);
            return entity;
        }

        public static String GetName<T>(this Table<T> table) where T : DbEntity<T>
        {
            return table.Context.Mapping.GetTable(typeof(T)).TableName;
        }

		public static bool IsHoliday(this DbDriver db, Date date)
		{
			return new DbDriver().Payments.CalendarDates.Any(a => a.IsBankHoliday && a.Date == date);
		}

		public static Date GetNextWorkingDay(this DbDriver db, Date date)
		{
			if (date.DateTime.DayOfWeek == DayOfWeek.Saturday) date.DateTime = date.DateTime.AddDays(2);
			if (date.DateTime.DayOfWeek == DayOfWeek.Sunday) date.DateTime = date.DateTime.AddDays(1);
			while (db.IsHoliday(date)) date.DateTime = date.DateTime.AddDays(1);
			return date;
		}

		public static Date GetPreviousWorkingDay(this DbDriver db, Date date)
		{
			if (date.DateTime.DayOfWeek == DayOfWeek.Saturday) date.DateTime = date.DateTime.AddDays(-1);
			if (date.DateTime.DayOfWeek == DayOfWeek.Sunday) date.DateTime = date.DateTime.AddDays(1 - 2);
			while (db.IsHoliday(date)) date.DateTime = date.DateTime.AddDays(-1);
			return date;
		}

		public static int[] GetDefaultPayDaysOfMonth(this DbDriver db)
		{
			var value = db.Ops.ServiceConfigurations.Single(a => a.Key == "Payments.PayDayPerMonth").Value;
			return value.Split(',').Select(Int32.Parse).ToArray();
		}
    }
}