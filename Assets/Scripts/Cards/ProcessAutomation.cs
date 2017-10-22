public class ProcessAutomation : ICard
{
    string ICard.GetImageAsset()
    {
        return "process-automation";
    }

    string ICard.GetName()
    {
        return "Process Automation";
    }

    bool ICard.PlayFromGrip()
    {
        Netrunner.game.runner.creditPool.Gain(2);
        Netrunner.game.runner.stack.Draw();
        return true;
    }
}
