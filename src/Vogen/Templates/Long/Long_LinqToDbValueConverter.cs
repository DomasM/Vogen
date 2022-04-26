﻿
        public class LinqToDbValueConverter : global::LinqToDB.Common.ValueConverter<VOTYPE, global::System.Int64>
        {
            public LinqToDbValueConverter()
                : base(
                      v => v.Value,
                      p => new VOTYPE(p),
                      handlesNulls: false)
            { }
        }