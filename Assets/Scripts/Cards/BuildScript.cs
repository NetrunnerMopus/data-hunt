public class BuildScript : ICard
{
    string ICard.GetImageAsset()
    {
        return "build-script";
    }

    string ICard.GetName()
    {
        return "Build Script";
    }

    bool ICard.PlayFromGrip()
    {
        Netrunner.game.runner.creditPool.Gain(1);
        var stack = Netrunner.game.runner.stack;
        stack.Draw();
        stack.Draw();
        return true;
    }
}
