using UnityEngine;

namespace Gameframe.StatSheet
{
    public interface IStatModifier<TKey>
    {
        float Modify(float inValue);
    }

    public struct StatModifier<TKey>
    {
        public TKey statType;
        public StatMode mode;
        public float value;

        public float Modify(float inValue)
        {
            return (mode == StatMode.Add) ? inValue + value : inValue * value;
        }

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
