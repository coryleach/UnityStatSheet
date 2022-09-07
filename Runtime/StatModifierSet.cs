using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// Set of stat modifiers
    /// Stat modifiers are adds or multipliers that can be applied to a stat sheet
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class StatModifierSet<TKey> : INotifyStatModifierSet<TKey>
    {
        private readonly List<StatModifier<TKey>> _mods = new List<StatModifier<TKey>>();

        /// <summary>
        /// Set a modifier
        /// </summary>
        /// <param name="modifier">Stat modifier value</param>
        public void Set(StatModifier<TKey> modifier)
        {
            Set(modifier.statType,modifier.value, modifier.mode);
        }

        /// <summary>
        /// Set a modifier
        /// </summary>
        /// <param name="statType">Stat type the modifier will affect</param>
        /// <param name="value">value of the stat modifier</param>
        /// <param name="mode">how the value will be applied to the stat</param>
        public void Set(TKey statType, float value, StatMode mode)
        {
            for (int i = 0; i < _mods.Count; i++)
            {
                if (_mods[i].statType.Equals(statType) && _mods[i].mode == mode)
                {
                    //If there is no change in value then just return and do nothing
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (_mods[i].value == value)
                    {
                        return;
                    }

                    var prevValue = _mods[i];
                    var newVal = prevValue;
                    newVal.value = value;
                    _mods[i] = newVal;
                    NotifyChanged(StatModifierSetActionType.Set, newVal, prevValue);
                    return;
                }
            }
            //Adding a new modifier
            var modifier = new StatModifier<TKey>()
            {
                statType = statType,
                value = value,
                mode = mode
            };
            _mods.Add(modifier);
            NotifyChanged(StatModifierSetActionType.Add, modifier);
        }

        /// <summary>
        /// Get modifier for a given stat type and mode
        /// </summary>
        /// <param name="statType">Stat that is being affected</param>
        /// <param name="mode">how the stat is applied: Add or Multiply</param>
        /// <returns>StatModifier value</returns>
        public StatModifier<TKey> Get(TKey statType, StatMode mode)
        {
            for (var i = 0; i < _mods.Count; i++)
            {
                if (_mods[i].statType.Equals(statType) && _mods[i].mode == mode)
                {
                    return _mods[i];
                }
            }

            switch (mode)
            {
                case StatMode.Multiply:
                    return new StatModifier<TKey>
                    {
                        statType = statType,
                        value = 1,
                        mode = StatMode.Multiply
                    };
                case StatMode.Add:
                    return new StatModifier<TKey>
                    {
                        statType = statType,
                        value = 0,
                        mode = StatMode.Add
                    };
                default:
                    throw new InvalidEnumArgumentException($"StatMode {mode} not implemented");
            }
        }

        public IEnumerable<StatModifier<TKey>> Get(StatMode mode)
        {
            return _mods.Where(x => x.mode == mode);
        }

        public int Count => _mods.Count;

        public StatModifier<TKey> GetIndex(int index)
        {
            return _mods[index];
        }

        public IEnumerator<StatModifier<TKey>> GetEnumerator()
        {
            return _mods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event StatModifierSetChangedEventHandler<TKey> ModifiersChanged;

        private void NotifyChanged(StatModifierSetActionType action, StatModifier<TKey> modifier, StatModifier<TKey> previous = new StatModifier<TKey>())
        {
            ModifiersChanged?.Invoke(this, new StatModifierSetChangedArgs<TKey>
            {
                Action = action,
                Modifier = modifier,
                Previous = previous
            });
        }
    }
}
