using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Gameframe.StatSheet.Tests
{
    public class StatModelTests
    {
        public enum TestStatType
        {
            Strength,
            Intelligence,
            Will
        }

        [Test]
        public void CanCreate()
        {
            var model = new StatModel<TestStatType>();
            Assert.NotNull(model);
        }

        [Test]
        public void BaseStats()
        {
            var model = new StatModel<TestStatType>();
            Assert.Null(model.BaseStats);
            //Adding this call to make sure updating totals will null base stats does not throw exception
            model.UpdateTotals();

            model.BaseStats = new ListStatSet<TestStatType>();
            Assert.NotNull(model.BaseStats);

            var baseStats = model.BaseStats as IStatSet<TestStatType>;
            Assert.IsTrue(baseStats != null);
            baseStats[TestStatType.Intelligence] = 10;
            Assert.IsTrue(Mathf.Approximately(model[TestStatType.Intelligence],0));
            model.UpdateTotals();
            Assert.IsTrue(Mathf.Approximately(model[TestStatType.Intelligence],10));
        }

        [Test]
        public void Modifiers_Add_and_Multiply()
        {
            var baseStats = new ListStatSet<TestStatType>();
            var model = new StatModel<TestStatType>
            {
                BaseStats = baseStats
            };

            baseStats[TestStatType.Intelligence] = 10;

            var modifierSet = new StatModifierSet<TestStatType>();
            model.AddModifierSet(modifierSet);
            modifierSet.Set(TestStatType.Intelligence, 10, StatMode.Add);

            model.UpdateTotals();

            Assert.IsTrue(Mathf.Approximately(model[TestStatType.Intelligence],20));

            modifierSet.Set(TestStatType.Intelligence, 5, StatMode.Add);
            model.UpdateTotals();
            Assert.IsTrue(Mathf.Approximately(model[TestStatType.Intelligence],15));

            modifierSet.Set(TestStatType.Intelligence, 2, StatMode.Multiply);
            model.UpdateTotals();
            Assert.IsTrue(Mathf.Approximately(model[TestStatType.Intelligence],30), $"Expected 30 but got {model[TestStatType.Intelligence]}");
        }

        [Test]
        public void IsDirty()
        {
            var baseStats = new ListStatSet<TestStatType>();
            var model = new StatModel<TestStatType>
            {
                BaseStats = baseStats
            };
            baseStats[TestStatType.Intelligence] = 10;

            //This works because StatModifierSet is a INotifyStatModifierSet
            var modifierSet = new StatModifierSet<TestStatType>();
            model.AddModifierSet(modifierSet);
            Assert.IsTrue(model.IsDirty);
            model.UpdateTotals();
            Assert.IsFalse(model.IsDirty);
            modifierSet.Set(TestStatType.Intelligence, 10, StatMode.Add);
            Assert.IsTrue(model.IsDirty);
            model.UpdateTotals();
            Assert.IsFalse(model.IsDirty);
            Assert.IsTrue(Mathf.Approximately(model[TestStatType.Intelligence],20));
        }

    }
}
