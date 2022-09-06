using System.Collections;
using System.Collections.Generic;

namespace Gameframe.StatSheet
{
    public class StatModel : StatModel<string> { }

    public class StatModel<TKey> : IReadOnlyStatSet<TKey>
    {
        protected IStatSet<TKey> _baseStats;
        public IStatSet<TKey> BaseStats
        {
            get => _baseStats;
            set => _baseStats = value;
        }

        protected ListStatSet<TKey> _statTotals = new ListStatSet<TKey>();
        public IStatSet<TKey> StatTotals => _statTotals;

        protected List<IStatModifierSet<TKey>> _modifiers = new List<IStatModifierSet<TKey>>();

        public virtual void AddModifierSet(IStatModifierSet<TKey> modifierSet)
        {
            _modifiers.Add(modifierSet);
        }

        public virtual void RemoveModifier(IStatModifierSet<TKey> modifierSet)
        {
            _modifiers.Remove(modifierSet);
        }

        public void UpdateTotals()
        {
            //Add Base Stats
            _statTotals.Clear();
            if (_baseStats != null)
            {
                _statTotals.Add(_baseStats);
            }

            //
            foreach (var modifierSet in _modifiers)
            {
                foreach (var mod in modifierSet)
                {
                    _statTotals[mod.statType] = mod.Modify(_statTotals[mod.statType]);
                }
            }
        }

        #region IReadonlyStatSet Implementation

        public float this[TKey statKey] => _statTotals[statKey];

        public IEnumerator<IStatValue<TKey>> GetEnumerator()
        {
            return _statTotals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }

}
