using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;

namespace SampleData
{
    public partial class HaveStatus
    {
        static volatile Dictionary<HaveStatusEnum, HaveStatus> _haveStatuses;
        static object _haveStatusesLock = new object();
        public static Dictionary<HaveStatusEnum, HaveStatus> All
        {
            get
            {
                if (_haveStatuses == null)
                {
                    lock (_haveStatusesLock)
                    {
                        if (_haveStatuses == null)
                        {
                            _haveStatuses = new Dictionary<HaveStatusEnum, HaveStatus>();
                            _.Db.For<HaveStatus>().FillEnumDictionary<HaveStatusEnum, HaveStatus>(_haveStatuses, "Status");
                        }
                    }
                }
                return _haveStatuses;
            }
        }

        public static implicit operator HaveStatusEnum(HaveStatus status)
        {
            HaveStatusEnum value = HaveStatusEnum.Invalid;
            Enum.TryParse<HaveStatusEnum>(status.Status, out value);

            return value;
        }

        public static HaveStatus Available
        {
            get
            {
                return All[HaveStatusEnum.Available];
            }
        }

        public static HaveStatus GivenAway
        {
            get
            {
                return All[HaveStatusEnum.GivenAway];
            }
        }

        public static HaveStatus Lost
        {
            get
            {
                return All[HaveStatusEnum.Lost];
            }
        }

        public static HaveStatus InUse
        {
            get
            {
                return All[HaveStatusEnum.InUse];
            }
        }

        public static HaveStatus Invalid
        {
            get
            {
                return All[HaveStatusEnum.Invalid];
            }
        }

        public static implicit operator HaveStatus(HaveStatusEnum status)
        {
            return HaveStatus.All[status];
        }
    }
}
