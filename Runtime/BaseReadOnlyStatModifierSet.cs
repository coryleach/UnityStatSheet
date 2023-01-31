using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// An immutable set of stat modifiers
    /// Stat modifiers are adds or multipliers that can be applied to a stat sheet
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseReadOnlyStatModifierSet<TKey> : IReadOnlyStatModifierSet<TKey>
    {
        /// <summary>
        /// Get modifier for a given stat type and mode
        /// </summary>
        /// <param name="statType">Stat that is being affected</param>
        /// <param name="mode">how the stat is applied: Add or Multiply</param>
        /// <returns>StatModifier value</returns>
        public IStatModifier<TKey> Get(TKey statType, StatMode mode)
        {
            foreach (var mod in this)
            {
                if (mod.StatType.Equals(statType) && mod.Mode == mode)
                {
                    return mod;
                }
            }

            switch (mode)
            {
                case StatMode.Multiply:
                    return new StatModifier<TKey>{
                        StatType = statType,
                        Value = 1,
                        Mode = StatMode.Multiply
                    };
                case StatMode.Add:
                    return new StatModifier<TKey>{
                        StatType = statType,
                        Value = 0,
                        Mode = StatMode.Add
                    };
                default:
                    throw new InvalidEnumArgumentException($"StatMode {mode} not implemented");
            }
        }

        public IEnumerable<IStatModifier<TKey>> Get(StatMode mode)
        {
            return this.Where(x => x.Mode == mode);
        }

        public abstract IEnumerator<IStatModifier<TKey>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
