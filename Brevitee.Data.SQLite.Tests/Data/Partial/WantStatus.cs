using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;

namespace SampleData
{
    public partial class WantStatus
    {
        static volatile Dictionary<WantStatusEnum, WantStatus> _wantStatuses;
        static object _wantStatusesLock = new object();
        public static Dictionary<WantStatusEnum, WantStatus> All
        {
            get
            {
                if (_wantStatuses == null)
                {
                    lock (_wantStatusesLock)
                    {
                        if (_wantStatuses == null)
                        {
                            _wantStatuses = new Dictionary<WantStatusEnum, WantStatus>();
                            _.Db.For<WantStatus>().FillEnumDictionary<WantStatusEnum, WantStatus>(_wantStatuses, "Status");
                        }
                    }
                }
                return _wantStatuses;
            }
        }

        public static implicit operator WantStatusEnum(WantStatus status)
        {
            WantStatusEnum value = WantStatusEnum.Invalid;
            Enum.TryParse<WantStatusEnum>(status.Status, out value);

            return value;
        }

        public static WantStatus Fulfilled
        {
            get
            {
                return All[WantStatusEnum.Fulfilled];
            }
        }

        public static WantStatus Unfulfilled
        {
            get
            {
                return All[WantStatusEnum.Unfulfilled];
            }
        }

        public static WantStatus DontWant
        {
            get
            {
                return All[WantStatusEnum.DontWant];
            }
        }

    }
}
