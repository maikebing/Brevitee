using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;

namespace Brevitee.Distributed
{
    public class Slot<T>: Slot where T: IRepositoryProvider
    {
        public Slot() : base() { }
        public Slot(T value)
            : base()
        {
            this.SetProvider(value);
        }

        public new T GetValue()
        {
            return (T)base.GetValue();
        }
    }

    public class Slot
    {
        public IRepositoryProvider Provider { get; set; }

        public T GetProvider<T>() where T: IRepositoryProvider
        {
            return (T)Provider;
        }

        public void SetProvider(IRepositoryProvider provider)
        {
            this.Provider = provider;
        }

        public Keyspace Keyspace
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// Gets the starting angle that this slot is valid for
        /// </summary>
        public double StartAngle { get; internal set; }

        /// <summary>
        /// Gets the ending angle that this slot is valid for
        /// </summary>
        public double EndAngle { get; internal set; }

        public double Degrees
        {
            get
            {
                return EndAngle - StartAngle;
            }
        }

        public double Radians
        {
            get
            {
                return Degrees * (Math.PI / 180);
            }
        }

        public Ring Parent { get; protected internal set; }

        public virtual object GetValue()
        {
            return this.Provider;
        }
    }
}
