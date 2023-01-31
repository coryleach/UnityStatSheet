using UnityEngine;

namespace Gameframe.StatSheet
{
    public interface IStatModifier<TKey>
    {
        TKey StatType { get; }
        StatMode Mode { get; }
        float Value { get; set; }
        float Modify(float inValue);
    }

    public struct StatModifier<TKey> : IStatModifier<TKey>
    {
        private TKey _statType;
        private StatMode _mode;
        private float _value;

        public TKey StatType
        {
            get => _statType;
            set => _statType = value;
        }

        public StatMode Mode
        {
            get => _mode;
            set => _mode = value;
        }

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public float Modify(float inValue)
        {
            return (_mode == StatMode.Add) ? inValue + _value : inValue * _value;
        }

        public static StatModifier<TKey> operator +(StatModifier<TKey> lhs, StatModifier<TKey> rhs)
        {
            Debug.Assert(lhs._statType.Equals(rhs._statType));
            Debug.Assert(lhs._mode == rhs._mode);
            return new StatModifier<TKey> {_statType = lhs._statType, _mode = lhs._mode, _value = lhs._value + rhs._value};
        }

        public static StatModifier<TKey> operator -(StatModifier<TKey> lhs, StatModifier<TKey> rhs)
        {
            Debug.Assert(lhs._statType.Equals(rhs._statType));
            Debug.Assert(lhs._mode == rhs._mode);
            return new StatModifier<TKey> {_statType = lhs._statType, _mode = lhs._mode, _value = lhs._value - rhs._value};
        }
    }
}
