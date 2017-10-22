public class Diesel : ICard
{
    string ICard.GetImageAsset()
    {
        return "diesel";
    }

    string ICard.GetName()
    {
        return "Diesel";
    }

    bool ICard.PlayFromGrip()
    {
        var stack = Netrunner.game.runner.stack;
        for (int i = 0; i < 3; i++)
        {
            stack.Draw();
        }
        return true;
    }
}
