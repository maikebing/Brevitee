using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Data
{
    public class QueryFilter: ParameterParser, IQueryFilter
    {
        protected List<IFilterToken> _filters;
        public QueryFilter()
        {
            this._filters = new List<IFilterToken>();
        }

        public QueryFilter(IFilterToken filter)
            : this()
        {
            this._filters.Add(filter);
        }

        public QueryFilter(string columnName)
            : this()
        {
            this.ColumnName = columnName;
        }

        protected internal string ColumnName { get; set; }

        public override IEnumerable<IFilterToken> Filters
        {
            get
            {
                return this._filters;
            }
        }

        public QueryFilter Add(IFilterToken c)
        {
            this._filters.Add(c);
            return this;
        }

        internal QueryFilter AddRange(IEnumerable<IFilterToken> filters)
        {
            this._filters.AddRange(filters);
            return this;
        }

        internal QueryFilter AddRange(QueryFilter builder)
        {
            this._filters.AddRange(builder.Filters);
            return this;
        }

        public QueryFilter StartsWith(object value)
        {
            this.Add(new StartsWithComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter EndsWith(object value)
        {
            this.Add(new EndsWithComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter Contains(object value)
        {
            this.Add(new ContainsComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter In(object[] values)
        {
            this.Add(new InComparison(this.ColumnName, values));
            return this;
        }

        public QueryFilter In(long[] values)
        {
            this.Add(new InComparison(this.ColumnName, values));
            return this;
        }

        public QueryFilter In(string[] values)
        {
            this.Add(new InComparison(this.ColumnName, values));
            return this;
        }

        public QueryFilter And(QueryFilter c)
        {
            return this.Add(new LiteralFilterToken(" AND "))
                .AddRange(c);
        }

        public QueryFilter Or(QueryFilter c)
        {
            return this.Add(new LiteralFilterToken(" OR "))
                .AddRange(c);
        }

        public static QueryFilter operator &(QueryFilter one, QueryFilter two)
        {
            return ParenConcat(one, " AND ", two);
        }

        public static QueryFilter operator |(QueryFilter one, QueryFilter two)
        {
            return ParenConcat(one, " OR ", two);
        }

        public static QueryFilter operator ==(QueryFilter c, object value)
        {
            Comparison comp = new Comparison(c.ColumnName, "=", value);
            if (value == null || value == DBNull.Value)
            {
                comp = new NullComparison(c.ColumnName, "IS");
            }
            c.Add(comp);
            return c;
        }

        public static QueryFilter operator !=(QueryFilter c, object value)
        {
            Comparison comp = new Comparison(c.ColumnName, "<>", value);
            if (value == null || value == DBNull.Value)
            {
                comp = new NullComparison(c.ColumnName, "IS NOT");
            }
            c.Add(comp);
            return c;
        }

        public static QueryFilter operator <(QueryFilter c, object value)
        {
            c.Add(new Comparison(c.ColumnName, "<", value));
            return c;
        }

        public static QueryFilter operator >(QueryFilter c, object value)
        {
            c.Add(new Comparison(c.ColumnName, ">", value));
            return c;
        }

        public static QueryFilter operator <=(QueryFilter c, object value)
        {
            c.Add(new Comparison(c.ColumnName, "<=", value));
            return c;
        }

        public static QueryFilter operator >=(QueryFilter c, object value)
        {
            c.Add(new Comparison(c.ColumnName, ">=", value));
            return c;
        }

        public static QueryFilter operator ==(QueryFilter c, DateTime dateTime)
        {
            c.Add(new Comparison(c.ColumnName, "=", dateTime));
            return c;
        }
        public static QueryFilter operator !=(QueryFilter c, DateTime dateTime)
        {
            c.Add(new Comparison(c.ColumnName, "<>", dateTime));
            return c;
        }
        public static QueryFilter operator <(QueryFilter c, DateTime dateTime)
        {
            c.Add(new Comparison(c.ColumnName, "<", dateTime));
            return c;
        }
        
        public static QueryFilter operator >(QueryFilter c, DateTime dateTime)
        {
            c.Add(new Comparison(c.ColumnName, ">", dateTime));
            return c;
        }
        public static QueryFilter operator <=(QueryFilter c, DateTime dateTime)
        {
            c.Add(new Comparison(c.ColumnName, "<=", dateTime));
            return c;
        }

        public static QueryFilter operator >=(QueryFilter c, DateTime dateTime)
        {
            c.Add(new Comparison(c.ColumnName, ">=", dateTime));
            return c;
        }

        public static bool operator true(QueryFilter e)
        {
            return false;
        }

        public static bool operator false(QueryFilter e)
        {
            return false;
        }

        private static QueryFilter ParenConcat(QueryFilter one, string middle, QueryFilter two)
        {
            QueryFilter newBuilder = new QueryFilter();
            newBuilder.Add(new OpenParen())
                .AddRange(one)
                .Add(new CloseParen())
                .Add(new LiteralFilterToken(middle))
                .Add(new OpenParen())
                .AddRange(two)
                .Add(new CloseParen());
            return newBuilder;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                QueryFilter o = obj as QueryFilter;
                if (o != null)
                {
                    return o.Parse().Equals(this.Parse());
                }
                else
                {
                    return base.Equals(obj);
                }
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.Parse().GetHashCode();
        }
    }

    public class QueryFilter<C> : QueryFilter where C : IFilterToken, new() // C column type
    {   
        public QueryFilter(): base()
        {
            
        }

        public QueryFilter(IFilterToken filter)
            : base(filter)
        {
        }

        public QueryFilter(string columnName)
            : base(columnName)
        {
        }
        
        public QueryFilter<C> Add(IFilterToken c)
        {
            this._filters.Add(c);            
            return this;
        }

        internal QueryFilter<C> AddRange(IEnumerable<IFilterToken> filters)
        {
            this._filters.AddRange(filters);
            return this;
        }

        internal QueryFilter<C> AddRange(QueryFilter<C> builder)
        {
            this._filters.AddRange(builder.Filters);
            return this;
        }

        public QueryFilter<C> StartsWith(object value)
        {
            this.Add(new StartsWithComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter<C> DoesntStartWith(object value)
        {
            this.Add(new DoesntStartWithComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter<C> DoesntEndWith(object value)
        {
            this.Add(new DoesntEndWithComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter<C> DoesntContain(object value)
        {
            this.Add(new DoesntContainComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter<C> EndsWith(object value)
        {
            this.Add(new EndsWithComparison(this.ColumnName, value));
            return this;
        }

        public QueryFilter<C> Contains(object value)
        {
            this.Add(new ContainsComparison(this.ColumnName, value));
            return this;
        }

        /// <summary>
        /// Adds an InComparison only if the specified object array is not empty
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public QueryFilter<C> InIfNotEmpty(object[] values)
        {
            if(values != null && values.Length > 0)
            {
                return In(values);
            }
            else
            {
                return this;
            }
        }

        public QueryFilter<C> In(object[] values)
        {
            this.Add(new InComparison(this.ColumnName, values));
            return this;
        }

        /// <summary>
        /// Adds an InComparison only if the specified object array is not empty
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public QueryFilter<C> InIfNotEmpty(long[] values)
        {
            if (values != null && values.Length > 0)
            {
                return In(values);
            }
            else
            {
                return this;
            }
        }
        public QueryFilter<C> In(long[] values)
        {
            this.Add(new InComparison(this.ColumnName, values));
            return this;
        }

        /// <summary>
        /// Adds an InComparison only if the specified object array is not empty
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public QueryFilter<C> InIfNotEmpty(string[] values)
        {
            if (values != null && values.Length > 0)
            {
                return In(values);
            }
            else
            {
                return this;
            }
        }

        public QueryFilter<C> In(string[] values)
        {
            this.Add(new InComparison(this.ColumnName, values));
            return this;
        }

        public QueryFilter<C> And(QueryFilter<C> c)
        {
            return this.Add(new LiteralFilterToken(" AND "))
                .AddRange(c);            
        }

        public QueryFilter<C> Or(QueryFilter<C> c)
        {
            return this.Add(new LiteralFilterToken(" OR "))
                .AddRange(c);
        }        

        public static QueryFilter<C> operator &(QueryFilter<C> one, QueryFilter<C> two)
        {
            return ParenConcat(one, " AND ", two);            
        }

        public static QueryFilter<C> operator |(QueryFilter<C> one, QueryFilter<C> two)
        {
            return ParenConcat(one, " OR ", two);
        }

        public static QueryFilter<C> operator ==(QueryFilter<C> c, object value)
        {
            Comparison comp = new Comparison(c.ColumnName, "=", value);
            if (value == null || value == DBNull.Value)
            {
                comp = new NullComparison(c.ColumnName, "IS");
            }
            c.Add(comp);
            return c;
        }

        public static QueryFilter<C> operator !=(QueryFilter<C> c, object value)
        {
            Comparison comp = new Comparison(c.ColumnName, "<>", value);
            if (value == null || value == DBNull.Value)
            {
                comp = new NullComparison(c.ColumnName, "IS NOT");
            }
            c.Add(comp);
            return c;
        }

        public static QueryFilter<C> operator <(QueryFilter<C> c, object value)
        {
            c.Add(new Comparison(c.ColumnName, "<", value));
            return c;   
        }

        public static QueryFilter<C> operator >(QueryFilter<C> c, object value)
        {
            c.Add(new Comparison(c.ColumnName, ">", value));
            return c;
        }

        public static QueryFilter<C> operator <=(QueryFilter<C> c, object value)
        {
            c.Add(new Comparison(c.ColumnName, "<=", value));
            return c;
        }

        public static QueryFilter<C> operator >=(QueryFilter<C> c, object value)
        {
            c.Add(new Comparison(c.ColumnName, ">=", value));
            return c;
        }

        public static bool operator true(QueryFilter<C> e)
        {
            return false;
        }

        public static bool operator false(QueryFilter<C> e)
        {
            return false;
        }

        private static QueryFilter<C> ParenConcat(QueryFilter<C> one, string middle, QueryFilter<C> two)
        {
            QueryFilter<C> newBuilder = new QueryFilter<C>();
            newBuilder.Add(new OpenParen())
                .AddRange(one)
                .Add(new CloseParen())
                .Add(new LiteralFilterToken(middle))
                .Add(new OpenParen())
                .AddRange(two)
                .Add(new CloseParen());
            return newBuilder;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                QueryFilter<C> o = obj as QueryFilter<C>;
                if (o != null)
                {
                    return o.Parse().Equals(this.Parse());
                }
                else
                {
                    return base.Equals(obj);
                }
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.Parse().GetHashCode();
        }
    }

}
