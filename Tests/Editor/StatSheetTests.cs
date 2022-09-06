using NUnit.Framework;
using UnityEngine;

namespace Gameframe.StatSheet.Tests
{
    public class StatSheetTests
    {
        [Test]
        public void CanCreate()
        {
            var statSheet = new ListStatSet();
            Assert.IsTrue(statSheet != null);
        }

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
        public void StatValue_Add()
        {
            var stat1 = new StatValue<string>() {StatType = "v1", Value = 1};
            var stat2 = new StatValue<string>() {StatType = "v1", Value = 2};
            var stat3 = stat1 + stat2;
            Assert.IsTrue(Mathf.Approximately(stat3.Value,3) && stat3.StatType == "v1",$"Expected 3 but got {stat3.Value}");
            Assert.IsTrue(Mathf.Approximately(stat1.Value,1) && stat1.StatType == "v1");
        }

        [Test]
        public void StatValue_Subtract()
        {
            var stat1 = new StatValue<string>() {StatType = "v1", Value = 1};
            var stat2 = new StatValue<string>() {StatType = "v1", Value = 2};
            var stat3 = stat1 - stat2;
            Assert.IsTrue(Mathf.Approximately(stat3.Value,-1) && stat3.StatType == stat1.StatType);
        }

    }

    public class ListStatTests
    {
        public enum StatType
        {
            Strength,
            Intelligence,
            Will
        }

        [Test]
        public void CanCreate()
        {
            var listSet = new ListStatSet();
            Assert.NotNull(listSet);
        }

        [Test]
        public void CanCreate_Generic()
        {
            var listSet = new ListStatSet<StatType>();
            Assert.NotNull(listSet);
        }

        [Test]
        public void SetterAndGetter()
        {
            var listSet = new ListStatSet();
            listSet["str"] = 10;
            Assert.IsTrue(Mathf.Approximately(listSet["str"] ,10f));
            listSet["str"] += 10;
            Assert.IsTrue(Mathf.Approximately(listSet["str"] ,20f));
        }

        [Test]
        public void SetterAndGetter_Generic()
        {
            var listSet = new ListStatSet<StatType>
            {
                [StatType.Strength] = 10
            };
            Assert.IsTrue(Mathf.Approximately(listSet[StatType.Strength] ,10f));
            listSet[StatType.Strength] += 10;
            Assert.IsTrue(Mathf.Approximately(listSet[StatType.Strength] ,20f));
        }

        [Test]
        public void AddSet()
        {
            var set1 = new ListStatSet
            {
                ["str"] = 10,
                ["sta"] = 5,
            };

            var set2 = new ListStatSet
            {
                ["str"] = 10,
                ["int"] = 6,
            };

            var sumSet = new ListStatSet
            {
                set1,
                set2
            };

            Assert.IsTrue(Mathf.Approximately(sumSet["str"] ,20f));
            Assert.IsTrue(Mathf.Approximately(sumSet["sta"] ,5f));
            Assert.IsTrue(Mathf.Approximately(sumSet["int"] ,6f));
        }

        [Test]
        public void SubtractSet()
        {
            var set1 = new ListStatSet
            {
                ["str"] = 10,
                ["sta"] = 5,
            };

            var set2 = new ListStatSet
            {
                ["str"] = 10,
                ["int"] = 6,
            };

            var sumSet = new ListStatSet();
            sumSet.Subtract(set1);
            sumSet.Subtract(set2);

            Assert.IsTrue(Mathf.Approximately(sumSet["str"] ,-20f));
            Assert.IsTrue(Mathf.Approximately(sumSet["sta"] ,-5f));
            Assert.IsTrue(Mathf.Approximately(sumSet["int"] ,-6f));
        }

        [Test]
        public void ListStatSetSerialize()
        {
            var set = new ListStatSet
            {
                ["str"] = 2
            };

            Assert.IsTrue(Mathf.Approximately(set["str"], 2));
            var serializedSet = JsonUtility.ToJson(set);
            var deserializedSet = JsonUtility.FromJson<ListStatSet>(serializedSet);
            Assert.IsTrue(Mathf.Approximately(deserializedSet["str"], 2),$"Error serializing/deserializing json: {serializedSet}");
        }
    }
}
