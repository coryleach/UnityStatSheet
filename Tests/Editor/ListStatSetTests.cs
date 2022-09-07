using NUnit.Framework;
using UnityEngine;

namespace Gameframe.StatSheet.Tests
{
    public class ListStatSetTests
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
