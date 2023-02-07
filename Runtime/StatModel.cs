using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gameframe.StatSheet
{
    /// <summary>
    ///
    /// </summary>
    public class StatModel : StatModel<string>
    {
    }

    public class StatModel<TKey> : IReadOnlyStatSet<TKey>
    {
        protected IReadOnlyStatSet<TKey> _baseStats;

        public IReadOnlyStatSet<TKey> BaseStats
        {
            get => _baseStats;
            set => _baseStats = value;
        }

        private bool _dirty = false;
        public bool IsDirty
        {
            get => _dirty;
            private set
            {
                if (_dirty == value)
                {
                    return;
                }
                _dirty = value;
                if (_dirty)
                {
                    OnBecameDirty?.Invoke();
                }
            }
        }

        public delegate void StatModelDelegate();

        public event StatModelDelegate OnBecameDirty;

        protected ListStatSet<TKey> _statTotals = new ListStatSet<TKey>();
        public IStatSet<TKey> StatTotals => _statTotals;

        protected List<IReadOnlyStatModifierSet<TKey>> _modifiers = new List<IReadOnlyStatModifierSet<TKey>>();

        public virtual void AddModifierSet(IReadOnlyStatModifierSet<TKey> modifierSet)
        {
            _modifiers.Add(modifierSet);
            //If this modifier set is a notify set then subscribe for changes
            if (modifierSet is INotifyStatModifierSet<TKey> notifySet)
            {
                notifySet.ModifiersChanged += NotifySetOnModifiersChanged;
            }
            IsDirty = true;
        }

        public virtual void RemoveModifierSet(IReadOnlyStatModifierSet<TKey> modifierSet)
        {
            _modifiers.Remove(modifierSet);
            if (modifierSet is INotifyStatModifierSet<TKey> notifySet)
            {
                notifySet.ModifiersChanged -= NotifySetOnModifiersChanged;
            }
            IsDirty = true;
        }

        public virtual void ClearModifiers()
        {
            foreach (var modifierSet in _modifiers)
            {
                if (modifierSet is INotifyStatModifierSet<TKey> notifySet)
                {
                    notifySet.ModifiersChanged -= NotifySetOnModifiersChanged;
                }
            }
            _modifiers.Clear();
            IsDirty = true;
        }

        private void NotifySetOnModifiersChanged(IReadOnlyStatModifierSet<TKey> set, StatModifierSetChangedArgs<TKey> args)
        {
            IsDirty = true;
        }

        /// <summary>
        /// Update all stat totals
        /// This method must be called to ensure all stats affected by modifiers are up to date
        /// </summary>
        /// <param name="checkDirty">When true totals will update only if IsDirty property is true</param>
        public virtual void UpdateTotals(bool checkDirty = false)
        {
            if (checkDirty && !IsDirty)
            {
                return;
            }

            //Add Base Stats
            _statTotals.Clear();
            if (_baseStats != null)
            {
                _statTotals.Add(_baseStats);
            }

            //Apply Adds
            foreach (var mod in _modifiers.SelectMany(modifierSet => modifierSet.Get(StatMode.Add)))
            {
                _statTotals[mod.StatType] = mod.Modify(_statTotals[mod.StatType]);
            }

            //Apply Multipliers
            foreach (var mod in _modifiers.SelectMany(modifierSet => modifierSet.Get(StatMode.Multiply)))
            {
                _statTotals[mod.StatType] = mod.Modify(_statTotals[mod.StatType]);
            }

            IsDirty = false;
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
