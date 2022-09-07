using NUnit.Framework;
using UnityEngine;

namespace Gameframe.StatSheet.Tests
{
    public class StatUtilityTests
    {
        [Test]
        public void StatUtility_Sum()
        {
            var set1 = new ListStatSet
            {
                ["sta"] = 10,
                ["int"] = 5,
            };

            var set2 = new ListStatSet
            {
                ["sta"] = 10,
            };

            var set3 = new ListStatSet
            {
                ["sta"] = 10,
            };

            var sumSet = StatSetUtility.Sum(set1, set2, set3);
            Assert.IsFalse(set1 == sumSet);
            Assert.IsFalse(set2 == sumSet);
            Assert.IsFalse(set3 == sumSet);

            Assert.IsTrue(Mathf.Approximately(set1["sta"],10));
            Assert.IsTrue(Mathf.Approximately(set2["sta"],10));
            Assert.IsTrue(Mathf.Approximately(set3["sta"],10));

            Assert.NotNull(sumSet);
            Assert.IsTrue(Mathf.Approximately(sumSet["sta"],30));
            Assert.IsTrue(Mathf.Approximately(sumSet["int"],5));
        }

        [Test]
        public void StatUtility__Diff()
        {
            var set1 = new ListStatSet
            {
                ["sta"] = 10,
                ["int"] = 5,
            };

            var set2 = new ListStatSet
            {
                ["sta"] = 10,
            };

            var diffSet = StatSetUtility.Diff(set1, set2);
            Assert.IsFalse(set1 == diffSet);
            Assert.IsFalse(set2 == diffSet);

            Assert.IsTrue(Mathf.Approximately(set1["sta"],10));
            Assert.IsTrue(Mathf.Approximately(set1["int"],5));
            Assert.IsTrue(Mathf.Approximately(set2["sta"],10));

            Assert.NotNull(diffSet);
            Assert.IsTrue(Mathf.Approximately(diffSet["sta"],0));
            Assert.IsTrue(Mathf.Approximately(diffSet["int"],5));
        }

        [Test]
        public void StatValue_Operator_Add()
        {
            var stat1 = new StatValue<string>() {StatType = "v1", Value = 1};
            var stat2 = new StatValue<string>() {StatType = "v1", Value = 2};
            var stat3 = stat1 + stat2;
            Assert.IsTrue(Mathf.Approximately(stat3.Value,3) && stat3.StatType == "v1",$"Expected 3 but got {stat3.Value}");
            Assert.IsTrue(Mathf.Approximately(stat1.Value,1) && stat1.StatType == "v1");
        }

        [Test]
        public void StatValue_Operator_Subtract()
        {
            var stat1 = new StatValue<string>() {StatType = "v1", Value = 1};
            var stat2 = new StatValue<string>() {StatType = "v1", Value = 2};
            var stat3 = stat1 - stat2;
            Assert.IsTrue(Mathf.Approximately(stat3.Value,-1) && stat3.StatType == stat1.StatType);
        }

    }
}
