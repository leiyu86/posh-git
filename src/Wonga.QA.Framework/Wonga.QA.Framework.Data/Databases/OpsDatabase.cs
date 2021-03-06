﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Simple.Data;

namespace Wonga.QA.Framework.Data
{
    public class OpsDatabase : QAFDatabase
    {
        public OpsDatabase(string connectionString)
            : base(connectionString)
        {
            
        }

		/// <summary>
		/// sets the configuration with a new value for the given key and returns the current value
		/// </summary>
		/// <typeparam name="T">generic type template</typeparam>
		/// <param name="keyName">the name of the key to update</param>
		/// <param name="value">the new value for the key</param>
		/// <returns>the current value</returns>
		public T SetServiceConfiguration<T>(string keyName, T value)
		{
			return SetServiceConfiguration(keyName, value, default(T));
		}

		public T SetServiceConfiguration<T>(string keyName, T value, T defaultValue)
		{
			var row = Db.ServiceConfigurations.FindByKey(keyName);

			var oldValue = row != null
							? ConvertToType<T>(row.Value)
							: defaultValue;
			Db.ServiceConfigurations.UpsertByKey(Key: keyName, Value: value.ToString());
			return oldValue;
		}

		public T GetServiceConfiguration<T>(string keyName, T defaultValue = default(T))
		{
			var row = Db.ServiceConfigurations.FindByKey(keyName);

			return row != null
			       	? ConvertToType<T>(row.Value)
			       	: defaultValue;
		}

    	private static T ConvertToType<T>(string value)
		{
			Type targetType = typeof(T);

			if (targetType.IsEnum)
			{
				return (T)Enum.Parse(targetType, value);
			}

			if (targetType == typeof(Guid))
			{
				Guid uniqueValue = Guid.Parse(value);
				return (T)Convert.ChangeType(uniqueValue, targetType, CultureInfo.InvariantCulture);
			}

			return (T)Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
		}
    }
}
