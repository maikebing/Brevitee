using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;

namespace SampleData
{
    public partial class GiveStatus
    {
        static Dictionary<GiveStatusEnum, GiveStatus> _haveStatuses;

        public static Dictionary<GiveStatusEnum, GiveStatus> All
        {
            get
            {
                if (_haveStatuses == null)
                {
                    _haveStatuses = new Dictionary<GiveStatusEnum, GiveStatus>();
                    _.Db.For<GiveStatus>().FillEnumDictionary<GiveStatusEnum, GiveStatus>(_haveStatuses, "Status");
                }
                return _haveStatuses;
            }
        }

        public static implicit operator GiveStatusEnum(GiveStatus status)
        {
            GiveStatusEnum value = GiveStatusEnum.Invalid;
            Enum.TryParse<GiveStatusEnum>(status.Status, out value);

            return value;
        }

        public static GiveStatus Invalid
        {
            get
            {
                return All[GiveStatusEnum.Invalid];
            }
        }

        public static GiveStatus Pending
        {
            get
            {
                return All[GiveStatusEnum.Pending];
            }
        }

        public static GiveStatus PendingConfirmation
        {
            get
            {
                return All[GiveStatusEnum.PendingConfirmation];
            }
        }

        public static GiveStatus Confirmed
        {
            get
            {
                return All[GiveStatusEnum.Confirmed];
            }
        }

    }
}
