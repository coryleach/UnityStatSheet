using System.Collections.Generic;
using System.Linq;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// DictionaryStatSet with string stat type
    /// </summary>
    public class DictionaryStatSet : DictionaryStatSet<string>
    {
    }

    /// <summary>
    /// Basic StatSet implementation using a dictionary
    /// Simple implementation but Non-Unity Serializable
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class DictionaryStatSet<TKey> : StatSet<TKey>
    {
        private readonly Dictionary<TKey, float> _dictionary = new Dictionary<TKey, float>();

        public override float this[TKey statType]
        {
            get
            {
                if (_dictionary.TryGetValue(statType, out float val))
                {
                    return val;
                }

                return 0;
            }
            set => _dictionary[statType] = value;
        }

        /// <summary>
        /// Check if a stat has ever been set in this dictionary
        /// </summary>
        /// <param name="statType"></param>
        /// <returns></returns>
        public bool HasStat(TKey statType)
        {
            return _dictionary.ContainsKey(statType);
        }

        public override IEnumerator<IStatValue<TKey>> GetEnumerator()
        {
            return _dictionary.Select(pair => (IStatValue<TKey>)new StatValue<TKey>
            {
                StatType = pair.Key,
                Value = pair.Value
            }).GetEnumerator();
        }
    }
}
