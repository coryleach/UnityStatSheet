namespace Gameframe.StatSheet
{
    /// <summary>
    /// Indexed set of stat modifiers for easy index enumeration
    /// </summary>
    /// <typeparam name="TKey">Stat key type (Usually an enum)</typeparam>
    public interface IStatModifierSetIndexed<TKey> : IReadOnlyStatModifierSet<TKey>
    {
        int Count { get; }
        StatModifier<TKey> GetIndex(int index);
    }
}
