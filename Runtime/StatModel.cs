using System.Collections.Generic;
using Gameframe.StatSheet.Generic;

namespace Gameframe.StatSheet
{
    public class StatModel : StatModel<string>
    {
    }
}

namespace Gameframe.StatSheet.Generic
{
    public class StatModel<TKey>
    {
        public StatModel() { }

        private IStatSet<TKey> _baseStats;
        public IStatSet<TKey> BaseStats
        {
            get => _baseStats;
            set => _baseStats = value;
        }

        private IStatSet<TKey> _statTotals;
        public IStatSet<TKey> StatTotals => _statTotals;

        private List<IStatModifierSet<TKey>> _modifiers = new List<IStatModifierSet<TKey>>();

        public void AddModifierSet(IStatModifierSet<TKey> modifierSet)
        {
            _modifiers.Add(modifierSet);
        }

        public void RemoveModifier(IStatModifierSet<TKey> modifierSet)
        {
            _modifiers.Remove(modifierSet);
        }

        public void UpdateTotals()
        {
            foreach (var modifierSet in _modifiers)
            {
                foreach (var mod in modifierSet)
                {
                    _statTotals[mod.statType] = modifierSet.Modify(mod.statType, _statTotals[mod.statType]);
                }
            }
        }
    }

}
