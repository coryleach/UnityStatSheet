namespace Gameframe.StatSheet
{
    public enum StatModifierSetActionType
    {
        Add,
        Set
    }

    public struct StatModifierSetChangedArgs<TKey>
    {
        public StatModifierSetActionType Action;
        public IStatModifier<TKey> Modifier;
        public IStatModifier<TKey> Previous;
    }

    public delegate void ReadOnlyStatModifierSetChangedEventHandler<TKey>(IReadOnlyStatModifierSet<TKey> modifierSet, StatModifierSetChangedArgs<TKey> args);

    /// <summary>
    /// Interface for a set of modifiers with an event for when modifies are changed or updated
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface INotifyReadOnlyStatModifierSet<TKey> : IReadOnlyStatSet<TKey>
    {
        event ReadOnlyStatModifierSetChangedEventHandler<TKey> ModifiersChanged;
    }

    public delegate void StatModifierSetChangedEventHandler<TKey>(IStatModifierSet<TKey> modifierSet, StatModifierSetChangedArgs<TKey> args);

    public interface INotifyStatModifierSet<TKey> : IStatModifierSet<TKey>
    {
        event StatModifierSetChangedEventHandler<TKey> ModifiersChanged;
    }
}
