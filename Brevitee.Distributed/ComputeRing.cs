using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Data;

namespace Brevitee.Distributed
{
    public class ComputeRing: Ring<ComputeNode>, IRepositoryProvider
    {
        public ComputeRing()
            : base()
        {
        }

        public ComputeRing(int slotCount)
            : base()
        {
            this.SetSlotCount(slotCount);
        }

        public void AddComputeNode(ComputeNode node)
        {
            AddSlot(new Slot<ComputeNode>(node));
        }
        

        protected internal override Slot CreateSlot()
        {
            return new Slot<ComputeNode>();
        }

        protected Slot FindSlot(object value)
        {
            int key = GetRepositoryKey(value);
            return FindSlot(key);
        }

        protected override Slot FindSlot(int key)
        {
            double slotIndex = Math.Floor((double)(key / SlotSize));
            Slot result = null;
            if (slotIndex < Slots.Length)
            {
                result = Slots[(int)slotIndex];
            }

            return result;
        }        

        #region IRepositoryProvider Members

        public void Create(object value)
        {
            IRepositoryProvider provider = GetRepositoryProvider(value);
            provider.Create(value);
        }

        public T Retrieve<T>(object keyOwner)
        {
            IRepositoryProvider provider = GetRepositoryProvider(keyOwner);
            return provider.Retrieve<T>(keyOwner);
        }

        public void Update(object value)
        {
            IRepositoryProvider provider = GetRepositoryProvider(value);
            provider.Update(value);
        }

        public void Delete(object value)
        {
            IRepositoryProvider provider = GetRepositoryProvider(value);
            provider.Delete(value);
        }

        public T[] Search<T>(object query)
        {
            List<T> results = new List<T>();
            Parallel.ForEach<Slot>(Slots, (s) =>
            {
                IRepositoryProvider provider = s.GetProvider<ComputeNode>();
                results.AddRange(provider.Search<T>(query));
            });

            return results.ToArray();
        }

        #endregion

        public override string GetHashString(object value)
        {
            Type type = value.GetType();
            
            PropertyInfo[] keyProperties = type.GetPropertiesWithAttributeOfType<RepositoryKey>();
            Type marker = keyProperties.Length > 0 ? typeof(RepositoryKey) : typeof(ColumnAttribute);

            StringBuilder stringToHashBuilder = GetHashStringFromProperties(value, new Type[] { marker }, type);
            string stringToHash = stringToHashBuilder.ToString();
            if (string.IsNullOrEmpty(stringToHash))
            {
                stringToHashBuilder = GetHashStringFromProperties(value, type);
                stringToHash = stringToHashBuilder.ToString();
            }

            return stringToHash.Sha1();
        }

        public override int GetRepositoryKey(object value)
        {
            int code = GetHashString(value).GetHashCode();
            code = code < 0 ? code * -1 : code;
            return code;
        }

        private static StringBuilder GetHashStringFromProperties(object value, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringToHash = new StringBuilder();
            properties.Each(prop =>
            {
                object val = prop.GetValue(value, null);
                string append = val == null ? "null" : val.ToString();
                stringToHash.Append(append);
            });

            return stringToHash;
        }

        private static StringBuilder GetHashStringFromProperties(object value, Type[] attributesAddorningPropertiesToScrape, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            StringBuilder stringToHash = new StringBuilder();
            properties.Each(prop =>
            {
                attributesAddorningPropertiesToScrape.Each(attrType =>
                {
                    if (prop.HasCustomAttributeOfType(attrType))
                    {
                        object val = prop.GetValue(value, null);
                        string append = val == null ? "null" : val.ToString();
                        stringToHash.Append(append);
                    }
                });
            });
            return stringToHash;
        }

        private IRepositoryProvider GetRepositoryProvider(object value)
        {
            Slot slot = FindSlot(value);
            IRepositoryProvider provider = slot.GetProvider<ComputeNode>();
            return provider;
        }
    }
}
