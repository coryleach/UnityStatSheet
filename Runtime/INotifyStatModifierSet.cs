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
        public StatModifier<TKey> Modifier;
        public StatModifier<TKey> Previous;
    }

    public delegate void StatModifierSetChangedEventHandler<TKey>(StatModifierSet<TKey> modifierSet, StatModifierSetChangedArgs<TKey> args);

    public interface INotifyStatModifierSet<TKey> : IStatModifierSet<TKey>
    {
        event StatModifierSetChangedEventHandler<TKey> ModifiersChanged;
    }
}
