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

    public delegate void StatModifierSetChangedEventHandler<TKey>(IStatModifierSet<TKey> modifierSet, StatModifierSetChangedArgs<TKey> args);

    public interface INotifyStatModifierSet<TKey> : IStatModifierSet<TKey>
    {
        event StatModifierSetChangedEventHandler<TKey> ModifiersChanged;
    }
}
