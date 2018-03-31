namespace model
{
    public interface ICost
    {
        /// <summary>
        /// Pays a cost.
        /// </summary>
        /// <returns>True if cost was paid.</returns>
        bool Pay(Game game);
    }
}