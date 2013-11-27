using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class MessageColumns: QueryFilter<MessageColumns>, IFilterToken
    {
        public MessageColumns() { }
        public MessageColumns(string columnName)
            : base(columnName)
        { }

        public MessageColumns Id
        {
            get
            {
                return new MessageColumns("Id");
            }
        }
        public MessageColumns To
        {
            get
            {
                return new MessageColumns("To");
            }
        }
        public MessageColumns Cc
        {
            get
            {
                return new MessageColumns("Cc");
            }
        }
        public MessageColumns Bcc
        {
            get
            {
                return new MessageColumns("Bcc");
            }
        }
        public MessageColumns Subject
        {
            get
            {
                return new MessageColumns("Subject");
            }
        }
        public MessageColumns Body
        {
            get
            {
                return new MessageColumns("Body");
            }
        }
        public MessageColumns Sent
        {
            get
            {
                return new MessageColumns("Sent");
            }
        }

        public MessageColumns FromUserId
        {
            get
            {
                return new MessageColumns("FromUserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}