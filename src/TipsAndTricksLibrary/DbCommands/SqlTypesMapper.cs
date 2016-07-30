namespace TipsAndTricksLibrary.DbCommands
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.SqlServer.Server;

    public static class SqlTypesMapper
    {
        private static readonly Dictionary<Type, SqlDbType> SqlTypesMap;

        private static readonly Dictionary<Type, Func<string, SqlMetaData>> SqlMetaDataCreators;

        private static Dictionary<Type, SqlDbType> AddMap<TProperty>(this Dictionary<Type, SqlDbType> mappings, SqlDbType sqlDbType)
        {
            var type = typeof(TProperty);
            mappings[type] = sqlDbType;

            if (default(TProperty) != null)
            {
                var nullableType = typeof(Nullable<>).MakeGenericType(type);
                mappings[nullableType] = sqlDbType;
            }
            return mappings;
        }

        private static SqlMetaData CreatorForScalarTypes(string name, Type type)
        {
            return new SqlMetaData(name, GetSqlType(type));
        }

        private static SqlMetaData CreatorForStringsAndBinary(string name, Type type)
        {
            return new SqlMetaData(name, GetSqlType(type), type == typeof(char) ? 1 : -1);
        }

        private static SqlMetaData CreatorForDecimal(string name, Type type)
        {
            return new SqlMetaData(name, GetSqlType(type), 10, 2);
        }

        private static Dictionary<Type, Func<string, SqlMetaData>> AddMap<TProperty>(this Dictionary<Type, Func<string, SqlMetaData>> mappings,
            Func<string, Type, SqlMetaData> creator)
        {
            var type = typeof(TProperty);
            mappings[type] = x => creator(x, type);

            if (default(TProperty) != null)
            {
                var nullableType = typeof(Nullable<>).MakeGenericType(type);
                mappings[nullableType] = x => creator(x, nullableType);
            }
            return mappings;
        }

        static SqlTypesMapper()
        {
            SqlTypesMap = new Dictionary<Type, SqlDbType>()
                .AddMap<byte>(SqlDbType.TinyInt)
                .AddMap<short>(SqlDbType.SmallInt)
                .AddMap<char>(SqlDbType.NChar)
                .AddMap<int>(SqlDbType.Int)
                .AddMap<long>(SqlDbType.BigInt)
                .AddMap<float>(SqlDbType.Real)
                .AddMap<double>(SqlDbType.Float)
                .AddMap<decimal>(SqlDbType.Decimal)
                .AddMap<bool>(SqlDbType.Bit)
                .AddMap<Guid>(SqlDbType.UniqueIdentifier)
                .AddMap<DateTime>(SqlDbType.DateTime)
                .AddMap<DateTimeOffset>(SqlDbType.DateTimeOffset)
                .AddMap<TimeSpan>(SqlDbType.Time)
                .AddMap<byte[]>(SqlDbType.VarBinary)
                .AddMap<char[]>(SqlDbType.NVarChar)
                .AddMap<string>(SqlDbType.NVarChar)
                .AddMap<object>(SqlDbType.Variant);

            SqlMetaDataCreators = new Dictionary<Type, Func<string, SqlMetaData>>()
                .AddMap<byte>(CreatorForScalarTypes)
                .AddMap<short>(CreatorForScalarTypes)
                .AddMap<char>(CreatorForStringsAndBinary)
                .AddMap<int>(CreatorForScalarTypes)
                .AddMap<long>(CreatorForScalarTypes)
                .AddMap<float>(CreatorForScalarTypes)
                .AddMap<double>(CreatorForScalarTypes)
                .AddMap<decimal>(CreatorForDecimal)
                .AddMap<bool>(CreatorForScalarTypes)
                .AddMap<Guid>(CreatorForScalarTypes)
                .AddMap<DateTime>(CreatorForScalarTypes)
                .AddMap<DateTimeOffset>(CreatorForScalarTypes)
                .AddMap<TimeSpan>(CreatorForScalarTypes)
                .AddMap<byte[]>(CreatorForStringsAndBinary)
                .AddMap<char[]>(CreatorForStringsAndBinary)
                .AddMap<string>(CreatorForStringsAndBinary)
                .AddMap<object>(CreatorForScalarTypes);
        }

        public static SqlDbType GetSqlType(Type type)
        {
            return SqlTypesMap[type];
        }

        public static SqlMetaData CreateMetaData(string name, Type type)
        {
            return SqlMetaDataCreators[type](name);
        }
    }
}