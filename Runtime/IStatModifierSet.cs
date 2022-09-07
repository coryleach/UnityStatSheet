using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// Enumerable Set of stat modifiers
    /// Generic version
    /// </summary>
    /// <typeparam name="TKey">Stat key type (Usually an enum)</typeparam>
    public interface IStatModifierSet<TKey> : IEnumerable<StatModifier<TKey>>
    {
        StatModifier<TKey> Get(TKey statName, StatMode mode);

        IEnumerable<StatModifier<TKey>> Get(StatMode mode);
    }
}
