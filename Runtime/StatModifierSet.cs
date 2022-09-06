using System.Collections;
using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// Set of stat modifiers
    /// Stat modifiers are adds or multipliers that can be applied to a stat sheet
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class StatModifierSet<TKey> : IStatModifierSetIndexed<TKey>
    {
        private readonly List<StatModifier<TKey>> _mods = new List<StatModifier<TKey>>();

        public void Set(TKey statType, float value, StatMode mode)
        {
            for (int i = 0; i < _mods.Count; i++)
            {
                if (_mods[i].statType.Equals(statType) && _mods[i].mode == mode)
                {
                    var val = _mods[i];
                    val.value = value;
                    _mods[i] = val;
                    return;
                }
            }
            //Adding a new modifier
            _mods.Add(new StatModifier<TKey>()
            {
                statType = statType,
                value = value,
                mode = mode
            });
        }

        public StatModifier<TKey> Get(TKey statName, StatMode mode)
        {
            for (var i = 0; i < _mods.Count; i++)
            {
                if (_mods[i].statType.Equals(statName) && _mods[i].mode == mode)
                {
                    return _mods[i];
                }
            }

            if (mode == StatMode.Multiply)
            {
                return new StatModifier<TKey>
                {
                    statType = statName,
                    value = 1,
                    mode = StatMode.Multiply
                };
            }
            else
            {
                return new StatModifier<TKey>
                {
                    statType = statName,
                    value = 0,
                    mode = StatMode.Add
                };
            }
        }

        public int Count => _mods.Count;

        public StatModifier<TKey> GetIndex(int index)
        {
            return _mods[index];
        }

        public float Modify(TKey statType, float value)
        {
            var adds = Get(statType, StatMode.Add);
            var mul = Get(statType, StatMode.Multiply);
            value += adds.value;
            value *= mul.value;
            return value;
        }

        public IEnumerator<StatModifier<TKey>> GetEnumerator()
        {
            return _mods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
