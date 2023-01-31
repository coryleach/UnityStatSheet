using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gameframe.StatSheet
{
    public class StatModifierSet<TKey> : BaseStatModifierSet<TKey>
    {
        private List<IStatModifier<TKey>> _mods = new List<IStatModifier<TKey>>();

        protected override IList<IStatModifier<TKey>> GetModifiersList()
        {
            return _mods;
        }

        protected override IStatModifier<TKey> CreateModifier(TKey statType, float value, StatMode mode)
        {
            var modifier = new StatModifier<TKey>()
            {
                StatType = statType,
                Value = value,
                Mode = mode
            };
            return modifier;
        }
    }

    /// <summary>
    /// Set of stat modifiers
    /// Stat modifiers are adds or multipliers that can be applied to a stat sheet
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseStatModifierSet<TKey> : INotifyStatModifierSet<TKey>
    {
        //private readonly List<StatModifier<TKey>> _mods = new List<StatModifier<TKey>>();

        protected abstract IList<IStatModifier<TKey>> GetModifiersList();

        protected abstract IStatModifier<TKey> CreateModifier(TKey statType, float value, StatMode mode);

        /// <summary>
        /// Set a modifier
        /// </summary>
        /// <param name="modifier">Stat modifier value</param>
        public void Set(IStatModifier<TKey> modifier)
        {
            Set(modifier.StatType,modifier.Value, modifier.Mode);
        }

        /// <summary>
        /// Set a modifier
        /// </summary>
        /// <param name="statType">Stat type the modifier will affect</param>
        /// <param name="value">value of the stat modifier</param>
        /// <param name="mode">how the value will be applied to the stat</param>
        public void Set(TKey statType, float value, StatMode mode)
        {
            var mods = GetModifiersList();

            for (int i = 0; i < mods.Count; i++)
            {
                if (mods[i].StatType.Equals(statType) && mods[i].Mode == mode)
                {
                    //If there is no change in value then just return and do nothing
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (mods[i].Value == value)
                    {
                        return;
                    }

                    var prevValue = mods[i];
                    var newVal = CreateModifier(prevValue.StatType, value, prevValue.Mode);
                    mods[i] = newVal;
                    NotifyChanged(StatModifierSetActionType.Set, newVal, prevValue);
                    return;
                }
            }
            //Adding a new modifier
            var modifier = CreateModifier(statType, value, mode);
            mods.Add(modifier);
            NotifyChanged(StatModifierSetActionType.Add, modifier);
        }

        /// <summary>
        /// Get modifier for a given stat type and mode
        /// </summary>
        /// <param name="statType">Stat that is being affected</param>
        /// <param name="mode">how the stat is applied: Add or Multiply</param>
        /// <returns>StatModifier value</returns>
        public IStatModifier<TKey> Get(TKey statType, StatMode mode)
        {
            var mods = GetModifiersList();

            for (var i = 0; i < mods.Count; i++)
            {
                if (mods[i].StatType.Equals(statType) && mods[i].Mode == mode)
                {
                    return mods[i];
                }
            }

            switch (mode)
            {
                case StatMode.Multiply:
                    return CreateModifier(statType, 1, mode);
                case StatMode.Add:
                    return CreateModifier(statType, 0, mode);
                default:
                    throw new InvalidEnumArgumentException($"StatMode {mode} not implemented");
            }
        }

        public IEnumerable<IStatModifier<TKey>> Get(StatMode mode)
        {
            return GetModifiersList().Where(x => x.Mode == mode).Cast<IStatModifier<TKey>>();
        }

        public int Count => GetModifiersList().Count;

        public IStatModifier<TKey> GetIndex(int index)
        {
            var mods = GetModifiersList();
            return mods[index];
        }

        public IEnumerator<IStatModifier<TKey>> GetEnumerator()
        {
            var mods = GetModifiersList();
            return mods.Cast<IStatModifier<TKey>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event StatModifierSetChangedEventHandler<TKey> ModifiersChanged;

        private void NotifyChanged(StatModifierSetActionType action, IStatModifier<TKey> modifier, IStatModifier<TKey> previous = null)
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
