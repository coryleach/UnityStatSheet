using System;
using UnityEngine;

namespace Gameframe.StatSheet
{
    public interface IStatModifier<TKey>
    {

    }

    public struct StatModifier<TKey>
    {
        public TKey statType;
        public StatMode mode;
        public float value;

        public static StatModifier<TKey> operator +(StatModifier<TKey> lhs, StatModifier<TKey> rhs)
        {
            Debug.Assert(lhs.statType.Equals(rhs.statType));
            Debug.Assert(lhs.mode == rhs.mode);
            return new StatModifier<TKey> {statType = lhs.statType, mode = lhs.mode, value = lhs.value + rhs.value};
        }

        public static StatModifier<TKey> operator -(StatModifier<TKey> lhs, StatModifier<TKey> rhs)
        {
            Debug.Assert(lhs.statType.Equals(rhs.statType));
            Debug.Assert(lhs.mode == rhs.mode);
            return new StatModifier<TKey> {statType = lhs.statType, mode = lhs.mode, value = lhs.value - rhs.value};
        }
    }
}

// namespace Gameframe.StatSheet
// {
//     [Serializable]
//     public struct StatModifier
//     {
//         public string statName;
//         public StatMode mode;
//         public float value;
//
//         public static StatModifier operator +(StatModifier lhs, StatModifier rhs)
//         {
//             Debug.Assert(lhs.statName == rhs.statName);
//             Debug.Assert(lhs.mode == rhs.mode);
//             return new StatModifier() {statName = lhs.statName, mode = lhs.mode, value = lhs.value + rhs.value};
//         }
//
//         public static StatModifier operator -(StatModifier lhs, StatModifier rhs)
//         {
//             Debug.Assert(lhs.statName == rhs.statName);
//             Debug.Assert(lhs.mode == rhs.mode);
//             return new StatModifier() {statName = lhs.statName, mode = lhs.mode, value = lhs.value - rhs.value};
//         }
//     }
// }
