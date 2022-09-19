using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    public interface IReadOnlyStatSet<TKey> : IEnumerable<IStatValue<TKey>>
    {
        float this[TKey statKey] { get; }
    }
}
