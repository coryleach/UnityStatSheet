using System;
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

            return new StatModifier<TKey>
            {
                statType = statName,
                value = 0,
                mode = mode
            };
        }

        public int Count => _mods.Count;

        public StatModifier<TKey> GetIndex(int index)
        {
            throw new NotImplementedException();
        }

        public float Modify(int index, float inValue)
        {
            throw new NotImplementedException();
        }

        public float Modify(TKey statType, float inValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<StatModifier<TKey>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
