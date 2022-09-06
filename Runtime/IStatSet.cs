using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    public interface IStatSet<TKey> : IEnumerable<IStatValue<TKey>>
    {
        public float this[TKey statKey] { get; set; }
    }
}
