namespace Gameframe.StatSheet
{
    public interface IStatSetIndexed<TKey> : IStatSet<TKey>
    {
        int Count { get; }
        IStatValue<TKey> GetIndex(int index);
    }
}
