using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    public interface IStatSet<TKey> : IEnumerable<IStatValue<TKey>>, IReadOnlyStatSet<TKey>
    {
        new float this[TKey statKey] { get; set; }
    }
}
