using System.Collections;
using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    // List of base stats
    // List of adds from n sources
    // List of multipliers from n sources
    // Generate/Calculate List of final stats
    // Need to Serialize just base stats
    // Equipment is just a list of modifiers
    // Need to be able to show stat delta (unequip modifier set A, equip set B and show how stats change)

    /// <summary>
    /// Abstract implementation of IStatSet<TKey>
    /// Provides Add and Subtract methods
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class StatSet<TKey> : IStatSet<TKey>
    {
        protected StatSet() { }

        protected StatSet(IStatSet<TKey> set)
        {
            foreach (var statValue in set)
            {
                this[statValue.StatType] = statValue.Value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="statType"></param>
        public abstract float this[TKey statType] { get; set; }

        /// <summary>
        /// Loops the given set and adds the value to this set.
        /// </summary>
        /// <param name="statSet">a stat set</param>
        public void Add(IStatSet<TKey> statSet)
        {
            foreach (var stat in statSet)
            {
                this[stat.StatType] += stat.Value;
            }
        }

        /// <summary>
        /// Loops over the given set and subtracts the values from the stats in this set
        /// </summary>
        /// <param name="statSet">a stat set</param>
        public void Subtract(IStatSet<TKey> statSet)
        {
            foreach (var stat in statSet)
            {
                this[stat.StatType] -= stat.Value;
            }
        }

        public abstract IEnumerator<IStatValue<TKey>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
