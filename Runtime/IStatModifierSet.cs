namespace Gameframe.StatSheet
{
    /// <summary>
    /// Enumerable set of stat modifiers
    /// Mutable version
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IStatModifierSet<TKey> : IReadOnlyStatModifierSet<TKey>
    {
        void Set(IStatModifier<TKey> modifier);
        void Set(TKey statType, float value, StatMode mode);
    }
}
