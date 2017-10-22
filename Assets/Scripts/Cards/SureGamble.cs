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

    bool ICard.PlayFromGrip()
    {
        var pool = Netrunner.game.runner.creditPool;
        pool.Pay(5);
        pool.Gain(9);
        return true;
    }
}
