using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace WeGout.Helpers
{
    internal static class Selectors
    {



        public static T SqlQuery<T>(this DatabaseFacade dbFacade, string sql, params object[] parameters)
        {
            return SqlQueryAsync<T>(dbFacade, sql, parameters).Result;
        }


        public static async Task<T> SqlQueryAsync<T>(this DatabaseFacade dbFacade, string sql, params object[] parameters)
        {
            T result = default(T);

            DbConnection connection = dbFacade.GetDbConnection();

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                await connection.OpenAsync();
            }

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;

                for (int i = 0; i < parameters.Length; i++)
                {
                    DbParameter param = command.CreateParameter();
                    param.ParameterName = $"@p{i}";
                    param.Value = parameters[i];

                    command.Parameters.Add(param);
                }

                if (typeof(T).GetConstructor(Type.EmptyTypes) != null &&
                    typeof(IList).IsAssignableFrom(typeof(T)))
                {
                    // Liste halinde veri
                    IList list = (IList)Activator.CreateInstance(typeof(T));
                    Type genericType = typeof(T).GenericTypeArguments[0];

                    bool isStruct = ObjectHelper.IsStruct(genericType);

                    using (DbDataReader dataReader = await command.ExecuteReaderAsync())
                    {
                        if (isStruct)
                        {
                            while (await dataReader.ReadAsync())
                            {
                                object value = ReadStructFromReader(genericType, dataReader);
                                list.Add(value);
                            }
                        }
                        else
                        {
                            while (await dataReader.ReadAsync())
                            {
                                object element = ReadClassFromReader(genericType, dataReader);
                                list.Add(element);
                            }
                        }
                    }

                    result = (T)list;
                }
                else
                {
                    // Tek satýr veri
                    Type type = typeof(T);

                    using (DbDataReader dataReader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await dataReader.ReadAsync())
                        {
                            result = ObjectHelper.IsStruct(typeof(T))
                                    ? (T)ReadStructFromReader(type, dataReader)
                                    : (T)ReadClassFromReader(type, dataReader);
                        }
                    }
                }
            }

            return result;
        }

        private static object ReadStructFromReader(Type type, DbDataReader dataReader)
        {
            object result = null;
            object dbValue = dataReader.GetValue(0);

            Type structType = Nullable.GetUnderlyingType(type) ?? type;

            if (dbValue != DBNull.Value)
            {
                if (dbValue.GetType() == structType)
                {
                    result = dbValue;
                }
                else
                {
                    // Tip dönüþümü
                    try
                    {
                        result = Convert.ChangeType(dbValue, structType);
                    }
                    catch { }
                }
            }

            return result;
        }

        private static object ReadClassFromReader(Type type, DbDataReader dataReader)
        {
            object result = Activator.CreateInstance(type);

            PropertyInfo[] properties = type.GetProperties();

            for (int ordinal = 0; ordinal < dataReader.FieldCount; ordinal++)
            {
                string name = dataReader.GetName(ordinal);

                var property = properties
                                .Where(l => string.Equals(l.Name, name, StringComparison.OrdinalIgnoreCase))
                                .FirstOrDefault();

                if (property != null)
                {
                    object dbValue = dataReader.GetValue(ordinal);
                    object value = null;

                    Type structType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    if (dbValue != DBNull.Value)
                    {
                        if (dbValue.GetType() == structType)
                        {
                            value = dbValue;
                        }
                        else
                        {
                            // Tip dönüþümü
                            try
                            {
                                value = Convert.ChangeType(dbValue, structType);
                            }
                            catch { }
                        }
                    }

                    try
                    {
                        property.SetValue(result, value);
                    }
                    catch { }
                }
            }

            return result;
        }
    


}
}
