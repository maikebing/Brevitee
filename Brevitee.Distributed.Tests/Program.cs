using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;
using Brevitee.Configuration;
using Brevitee.Drawing;
using Brevitee.Html;
using Brevitee.Logging;
using Brevitee.Testing;
using Brevitee.Distributed;
using System.Net;
using System.Reflection;

namespace Brevitee.Distributed.Tests
{
    [Serializable]
    class Program: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
        }

        class TestRing : Ring
        {
            public bool SetSlotsWasCalled { get; set; }
            protected override void InitializeSlots(int count)
            {
                SetSlotsWasCalled = true;
            }

            protected internal override Slot CreateSlot()
            {
                throw new NotImplementedException();
            }

            public override string GetHashString(object value)
            {
                throw new NotImplementedException();
            }

            public override int GetRepositoryKey(object value)
            {
                throw new NotImplementedException();
            }

            protected override Slot FindSlot(int key)
            {
                throw new NotImplementedException();
            }
        }

        [UnitTest]
        public void SettingSlotCountShouldCallSetSlots()
        {
            TestRing ring = new TestRing();
            Expect.IsFalse(ring.SetSlotsWasCalled);
            ring.SetSlotCount(360);
            Expect.IsTrue(ring.SetSlotsWasCalled);
        }

        [UnitTest]
        public void SetSlotCountShouldSetSlotLength()
        {
            ComputeRing ring = new ComputeRing();
            ring.SetSlotCount(2);
            Expect.AreEqual(2, ring.Slots.Length);
        }

        [UnitTest]
        public void SlotShouldMakeFullCircleAfterInit()
        {
            ComputeRing ring = new ComputeRing();
            int slotCount = RandomNumber.Between(8, 16);
            ring.SetSlotCount(slotCount);
            PrintSlots(ring);

            Expect.AreEqual(slotCount, ring.Slots.Length);
            double fullCircle = 360;
            double endAngle = ring.Slots[ring.Slots.Length - 1].EndAngle;
            Expect.AreEqual(fullCircle, endAngle);
        }

        [UnitTest]
        public void SlotShouldMakeFullCircleAfterInit13()
        {
            ComputeRing ring = new ComputeRing();
            int slotCount = 13;//RandomNumber.Between(8, 16);
            ring.SetSlotCount(slotCount);
            PrintSlots(ring);

            Expect.AreEqual(slotCount, ring.Slots.Length);
            double fullCircle = 360;
            double endAngle = ring.Slots[ring.Slots.Length - 1].EndAngle;
            Expect.AreEqual(fullCircle, endAngle);
        }

        private static void PrintSlots(ComputeRing ring)
        {
            ring.Slots.Each((s, i) =>
            {
                OutLineFormat("Slot {0}", ConsoleColor.Blue, i);
                OutLineFormat("\tstart angle: {0}", ConsoleColor.White, s.StartAngle);
                OutLineFormat("\t  end angle: {0}", ConsoleColor.Yellow, s.EndAngle);
                OutLineFormat("\tstart key: {0}", ConsoleColor.Cyan, s.Keyspace.Start);
                OutLineFormat("\t  end key: {0}", ConsoleColor.Cyan, s.Keyspace.End);
            });
        }

        [UnitTest]
        public void AddComputeNodeShouldAddSlot()
        {
            int slotCount = RandomNumber.Between(8, 16);
            ComputeRing ring = new ComputeRing(slotCount);
            ring.AddComputeNode(new ComputeNode());
            Expect.AreEqual(slotCount + 1, ring.Slots.Length);

            PrintSlots(ring);
        }

        [UnitTest]
        public void ComputeNodeFromCurrentHostShouldBeSelf()
        {
            ComputeNode current = ComputeNode.FromCurrentHost();
            string hostName = Dns.GetHostName();
            Expect.AreEqual(hostName, current.HostName);
            object info = current.GetInfo();
            Expect.AreEqual(hostName, info.Property<string>("HostName"));
            OutLine(info.PropertiesToString());
        }

        [UnitTest]
        public void ComputeNodeGetInfoStringsTest()
        {
            ComputeNode current = ComputeNode.FromCurrentHost();
            Dictionary<string, string> info = current.GetInfoDictionary();
            info.Keys.Each((k) =>
            {
                OutLineFormat("Key: {0}, Val: {1}", k, info[k]);
            });
        }

        public void Before()
        {
            OutLine("BEFORE RAN", ConsoleColor.Cyan);
        }

        public void After()
        {
            OutLine("AFTER RAN", ConsoleColor.Cyan);
        }

        [UnitTest]
        public void ComputeNodeFromCurrentShouldHaveHostname()
        {
            ComputeNode current = ComputeNode.FromCurrentHost();
            Expect.AreEqual(current.HostName, Dns.GetHostName());
        }

        [UnitTest("Set Slot count shouldn't overwrite existing slots", "Before", "After")]
        public void SetSlotCountShouldKeepExistingSlots()
        {
            ComputeRing ring = new ComputeRing();

            ComputeNode node = new ComputeNode();
            node.HostName = "HostName_".RandomLetters(4);
            ring.AddComputeNode(node);
            Expect.AreEqual(1, ring.Slots.Length);

            ComputeNode check = ring.Slots[0].GetProvider<ComputeNode>();
            Expect.IsNotNull(check);

            ring.SetSlotCount(3);

            Expect.AreEqual(3, ring.Slots.Length);
            check = ring.Slots[0].GetProvider<ComputeNode>();
            
            Expect.AreEqual(node.HostName, check.HostName);

            PrintSlots(ring);
        }

    }
}
