namespace Gameframe.StatSheet
{
    /// <summary>
    /// Utilities for performing operations on stat sets
    /// </summary>
    public static class StatSetUtility
    {
        /// <summary>
        /// Sum a list of IStatSets
        /// </summary>
        /// <param name="sets"></param>
        /// <typeparam name="TKey">The type of the Stat. Usually an enum.</typeparam>
        /// <returns>IStatSetIndexed<TKey></returns>
        public static IStatSetIndexed<TKey> Sum<TKey>(params IStatSet<TKey>[] sets)
        {
            var newSet = new ListStatSet<TKey>();
            for (var i = 0; i < sets.Length; i++)
            {
                newSet.Add(sets[i]);
            }
            return newSet;
        }

        /// <summary>
        /// Diff two sets
        /// Generic version
        /// </summary>
        /// <param name="lhs">left hand side</param>
        /// <param name="rhs">right hand side</param>
        /// <typeparam name="TKey">Type of the stat. Usually an enum.</typeparam>
        /// <returns>IStatSetIndexed<TKey></returns>
        public static IStatSetIndexed<TKey> Diff<TKey>(IStatSet<TKey> lhs, IStatSet<TKey> rhs)
        {
            var newSet = new ListStatSet<TKey>(lhs);
            newSet.Subtract(rhs);
            return newSet;
        }
    }
}
