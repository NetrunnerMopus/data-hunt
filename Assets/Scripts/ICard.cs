public interface ICard
{
    /// <summary>
    /// Plays the card effect.
    /// </summary>
    /// <returns>True if the card should go to heap.</returns>
    bool PlayFromGrip();
    string GetName();
    string GetImageAsset();
}
