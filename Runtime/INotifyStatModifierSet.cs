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

    public delegate void StatModifierSetChangedEventHandler<TKey>(IReadOnlyStatModifierSet<TKey> modifierSet, StatModifierSetChangedArgs<TKey> args);

    /// <summary>
    /// Interface for a mutable set of stat modifiers that provides an event callback to notify when modifiers change
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface INotifyStatModifierSet<TKey> : IStatModifierSet<TKey>
    {
        event StatModifierSetChangedEventHandler<TKey> ModifiersChanged;
    }
}
