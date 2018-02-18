public class QualityTime : ICard
{
    string ICard.GetImageAsset()
    {
        return "quality-time";
    }

    string ICard.GetName()
    {
        return "Quality Time";
    }

    bool ICard.PlayFromGrip()
    {
        Netrunner.game.runner.creditPool.Pay(3);
        var stack = Netrunner.game.runner.stack;
        for (int i = 0; i < 5; i++)
        {
            stack.Draw();
        }
        return true;
    }
}
