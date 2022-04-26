﻿
        public class DapperTypeHandler : global::Dapper.SqlMapper.TypeHandler<VOTYPE>
        {
            public override void SetValue(global::System.Data.IDbDataParameter parameter, VOTYPE value)
            {
                parameter.DbType = global::System.Data.DbType.Single;
                parameter.Value = value.Value;
            }

            public override VOTYPE Parse(object value)
            {
                return value switch
                {
                    global::System.Single floatValue => new VOTYPE(floatValue),
                    global::System.Double doubleValue when doubleValue < global::System.Single.MaxValue => new VOTYPE((global::System.Single)doubleValue),
                    global::System.Int32 intValue => new VOTYPE(intValue),
                    global::System.Int64 longValue when longValue < global::System.Single.MaxValue => new VOTYPE((global::System.Single)longValue),
                    global::System.String stringValue when !global::System.String.IsNullOrEmpty(stringValue) && global::System.Single.TryParse(stringValue, out var result) => new VOTYPE(result),
                    _ => throw new global::System.InvalidCastException($"Unable to cast object of type {value.GetType()} to VOTYPE"),
                };
            }
        }