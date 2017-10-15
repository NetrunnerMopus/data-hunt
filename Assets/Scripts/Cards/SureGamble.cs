public class SureGamble : ICard
{
    string ICard.GetImageAsset()
    {
        return "sure-gamble";
    }

    string ICard.GetName()
    {
        return "Sure Gamble";
    }

    void ICard.Play()
    {
        var pool = Netrunner.game.runner.creditPool;
        pool.Pay(5);
        pool.Gain(9);
    }
}
