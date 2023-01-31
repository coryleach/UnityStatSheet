using NUnit.Framework;
using UnityEngine;

namespace Gameframe.StatSheet.Tests
{
    public class StatModifierSetTests
    {
        public enum StatType
        {
            Str,
            Int
        }

        [Test]
        public void CanCreate()
        {
            var modSet = new StatModifierSet<StatType>();
            Assert.NotNull(modSet);
        }

        [Test]
        public void Set_and_Get_Add_Modify()
        {
            var modSet = new StatModifierSet<StatType>();
            //Add additive Str modifier
            modSet.Set(StatType.Str, 1, StatMode.Add);

            var modifier = modSet.Get(StatType.Str, StatMode.Add);
            Assert.IsTrue(modifier.Mode == StatMode.Add);
            Assert.IsTrue(modifier.StatType == StatType.Str);
            Assert.IsTrue(Mathf.Approximately(1, modifier.Value));

            var moddedValue = modifier.Modify(1);
            Assert.IsTrue(Mathf.Approximately(2, moddedValue));
        }

        [Test]
        public void Set_and_Get_Mul_Modify()
        {
            var modSet = new StatModifierSet<StatType>();
            //Add additive Str modifier
            modSet.Set(StatType.Str, 5, StatMode.Multiply);

            var modifier = modSet.Get(StatType.Str, StatMode.Multiply);
            Assert.IsTrue(modifier.Mode == StatMode.Multiply);
            Assert.IsTrue(modifier.StatType == StatType.Str);
            Assert.IsTrue(Mathf.Approximately(5, modifier.Value));

            var moddedValue = modifier.Modify(2);
            Assert.IsTrue(Mathf.Approximately(10, moddedValue));
        }

        [Test]
        public void ModifiersChangedEvent_Action_Add()
        {
            var modSet = new StatModifierSet<StatType>();

            bool notified = false;

            modSet.ModifiersChanged += (set, args) =>
            {
                notified = true;
                Assert.IsTrue(set == modSet);
                Assert.IsTrue(args.Action == StatModifierSetActionType.Add);
                Assert.IsTrue(args.Modifier.StatType == StatType.Int);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                Assert.IsTrue(args.Modifier.Value == 2);
                Assert.IsTrue(args.Modifier.Mode == StatMode.Multiply);
            };

            modSet.Set(StatType.Int, 2, StatMode.Multiply);

            //Ensure callback was actually called
            Assert.IsTrue(notified);
        }

        [Test]
        public void ModifiersChangedEvent_Action_Set()
        {
            var modSet = new StatModifierSet<StatType>();

            modSet.Set(StatType.Int, 2, StatMode.Multiply);

            bool notified = false;

            modSet.ModifiersChanged += (set, args) =>
            {
                notified = true;
                Assert.IsTrue(set == modSet);
                Assert.IsTrue(args.Action == StatModifierSetActionType.Set);
                Assert.IsTrue(args.Modifier.StatType == StatType.Int);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                Assert.IsTrue(args.Modifier.Value == 3);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                Assert.IsTrue(args.Previous.Value == 2, $"Expected Value = 2 but Value = {args.Previous.Value }");
                Assert.IsTrue(args.Previous.StatType == StatType.Int);
                Assert.IsTrue(args.Previous.Mode == StatMode.Multiply);
                Assert.IsTrue(args.Modifier.Mode == StatMode.Multiply);
            };

            modSet.Set(StatType.Int, 3, StatMode.Multiply);

            //Ensure callback was actually called
            Assert.IsTrue(notified);
        }

        [Test]
        public void ModifiersChangedEvent_Action_Set_Unchanged()
        {
            var modSet = new StatModifierSet<StatType>();

            modSet.Set(StatType.Int, 2, StatMode.Multiply);

            bool notified = false;

            modSet.ModifiersChanged += (set, args) =>
            {
                notified = true;
            };

            modSet.Set(StatType.Int, 2, StatMode.Multiply);

            //Value didn't change so we should NOT be notified of anything
            Assert.IsFalse(notified);
        }
    }
}
