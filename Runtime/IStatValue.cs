using UnityEngine;

namespace Gameframe.StatSheet
{
    public interface IStatValue<TKey>
    {
        TKey StatType { get; }
        float Value { get; }

        // Operators in interfaces not available until C# 8.0+
        // public static IStatValue<TKey> operator +(IStatValue<TKey> lhs, IStatValue<TKey> rhs)
        // {
        //     Debug.Assert(lhs.StatType.Equals(rhs.StatType), "stat types do not match");
        //     return new StatValue<TKey>
        //     {
        //         StatType = lhs.StatType,
        //         Value = (lhs.Value + rhs.Value)
        //     };
        // }
        //
        // public static IStatValue<TKey> operator -(IStatValue<TKey> lhs, IStatValue<TKey> rhs)
        // {
        //     Debug.Assert(lhs.StatType.Equals(rhs.StatType), "stat types do not match");
        //     return new StatValue<TKey>
        //     {
        //         StatType = lhs.StatType,
        //         Value = (lhs.Value - rhs.Value)
        //     };
        // }
    }
}
