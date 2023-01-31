using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    /// <summary>
    /// Mutable set of stat modifiers
    /// </summary>
    /// <typeparam name="TKey">Stat key type</typeparam>
    public class StatModifierSet<TKey> : BaseStatModifierMutableSet<TKey>
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
}
