using UnityEngine;

namespace Gameframe.StatSheet
{
    public struct StatValue<TKey> : IStatValue<TKey>
    {
        [SerializeField]
        private TKey _statType;
        [SerializeField]
        private float _value;

        public TKey StatType
        {
            get => _statType;
            set => _statType = value;
        }

        public float Value
        {
            get => _value;
            set => this._value = value;
        }

        public static StatValue<TKey> operator +(StatValue<TKey> lhs, StatValue<TKey> rhs)
        {
            Debug.Assert(lhs._statType.Equals(rhs._statType), "stat types do not match");
            return new StatValue<TKey>
            {
                _statType = lhs._statType,
                _value = (lhs._value + rhs._value)
            };
        }

        public static StatValue<TKey> operator -(StatValue<TKey> lhs, StatValue<TKey> rhs)
        {
            Debug.Assert(lhs._statType.Equals(rhs._statType), "stat types do not match");
            return new StatValue<TKey>
            {
                _statType = lhs._statType,
                _value = (lhs._value - rhs._value)
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is StatValue<TKey> other)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                return other._statType.Equals(_statType) && other._value == _value;
            }
            return false;
        }

        public override string ToString()
        {
            return $"({_statType}:{_value})";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
