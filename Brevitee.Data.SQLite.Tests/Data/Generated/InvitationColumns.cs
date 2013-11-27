using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class InvitationColumns: QueryFilter<InvitationColumns>, IFilterToken
    {
        public InvitationColumns() { }
        public InvitationColumns(string columnName)
            : base(columnName)
        { }

        public InvitationColumns Id
        {
            get
            {
                return new InvitationColumns("Id");
            }
        }
        public InvitationColumns LastModifiedBy
        {
            get
            {
                return new InvitationColumns("LastModifiedBy");
            }
        }
        public InvitationColumns LastModifiedDate
        {
            get
            {
                return new InvitationColumns("LastModifiedDate");
            }
        }
        public InvitationColumns Sent
        {
            get
            {
                return new InvitationColumns("Sent");
            }
        }

        public InvitationColumns InviterId
        {
            get
            {
                return new InvitationColumns("InviterId");
            }
        }
        public InvitationColumns InviteeId
        {
            get
            {
                return new InvitationColumns("InviteeId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}