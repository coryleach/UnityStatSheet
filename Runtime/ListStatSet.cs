using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// StatSheet implemented using a List
    /// Supports iterating over the stats using an index value
    /// Unity-Serializable
    /// </summary>
    [Serializable]
    public sealed class ListStatSet : ListStatSet<string>, ISerializationCallbackReceiver
    {
        public void OnBeforeSerialize()
        {
            //Remove any duplicate keys
            var namesSet = new HashSet<string>();
            for (var i = stats.Count-1; i >= 0; i--)
            {
                var stat = stats[i];
                if (namesSet.Contains(stat.statType))
                {
                    stats.RemoveAt(i);
                }
                else
                {
                    namesSet.Add(stat.statType);
                }
            }
        }

        public void OnAfterDeserialize()
        {
            //We need to build indexTable after deserializing because it is non-serialized
            IndexTable.Clear();
            for (var i = 0; i < stats.Count; i++)
            {
                var stat = stats[i];
                IndexTable[stat.statType] = i;
            }
        }
    }

    /// <summary>
    /// StatSheet implemented using a List
    /// Supports iterating over the stats using an index value
    /// Non-unity-serializable due to generics
    /// </summary>
    public class ListStatSet<TKey> : StatSet<TKey>, IStatSetIndexed<TKey>
    {
        [Serializable]
        public struct StatValue : IStatValue<TKey>
        {
            public TKey statType;
            public float value;

            public TKey StatType => statType;
            public float Value => value;
        }

        /// <summary>
        /// List of stat values
        /// Serialization callbacks will remove duplicate stat names if found
        /// Duplicate stat names should only be possible if manually added via the Unity inspector
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        // If we make this readonly it seems to become unserializable so don't do that pls
        protected List<StatValue> stats = new List<StatValue>();

        /// <summary>
        /// Maintain a non-serialized mapping of statName to index in stats list
        /// This is to ensure quick lookups for large stat sets
        /// This requires that we implement ISerializationCallbackReceiver to build this table on deserialization
        /// </summary>
        [NonSerialized]
        protected readonly Dictionary<TKey, int> IndexTable = new Dictionary<TKey, int>();

        public ListStatSet() { }

        public ListStatSet(IStatSet<TKey> set) : base(set) { }

        public override float this[TKey statType]
        {
            get
            {
                if (IndexTable.TryGetValue(statType, out int index))
                {
                    return stats[index].value;
                }
                return 0;
            }
            set
            {
                if (IndexTable.TryGetValue(statType, out int index))
                {
                    var stat = stats[index];
                    stat.value = value;
                    stats[index] = stat;
                    return;
                }
                //Add stat to list if we didn't find one to modify
                stats.Add(new StatValue
                {
                    statType = statType,
                    value = value
                });
                //Add to index table
                IndexTable[statType] = stats.Count - 1;
            }
        }

        public int Count => stats.Count;

        public void Clear()
        {
            IndexTable.Clear();
            stats.Clear();
        }

        public IStatValue<TKey> GetIndex(int index)
        {
            return stats[index];
        }

        public override IEnumerator<IStatValue<TKey>> GetEnumerator()
        {
            return stats.Select(x => (IStatValue<TKey>)x).GetEnumerator();
        }
    }
}
