namespace model.player
{
    // CR: 10.2
    public enum Information
    {
        // CR: 10.2.2
        HIDDEN_FROM_ALL,
        HIDDEN_FROM_CORP,
        HIDDEN_FROM_RUNNER,
        // CR: 10.2.3
        OPEN
    }

    // CR: 10.2.2
    // CR: 10.2.3
    public interface IPerception
    {
        bool CanSee(Information information);
    }

    public class CorpPerception : IPerception
    {
        bool IPerception.CanSee(Information information)
        {
            switch (information)
            {
                case Information.OPEN:
                case Information.HIDDEN_FROM_RUNNER:
                    return true;
                case Information.HIDDEN_FROM_ALL:
                case Information.HIDDEN_FROM_CORP:
                    return false;
                default:
                    throw new System.ArgumentException(information.ToString());
            }
        }
    }

    public class RunnerPerception : IPerception
    {
        bool IPerception.CanSee(Information information)
        {
            switch (information)
            {
                case Information.OPEN:
                case Information.HIDDEN_FROM_CORP:
                    return true;
                case Information.HIDDEN_FROM_ALL:
                case Information.HIDDEN_FROM_RUNNER:
                    return false;
                default:
                    throw new System.ArgumentException(information.ToString());
            }
        }
    }

    public class SpectatorPerception : IPerception
    {
        bool IPerception.CanSee(Information information)
        {
            switch (information)
            {
                case Information.OPEN:
                    return true;
                case Information.HIDDEN_FROM_ALL:
                case Information.HIDDEN_FROM_RUNNER:
                case Information.HIDDEN_FROM_CORP:
                    return false;
                default:
                    throw new System.ArgumentException(information.ToString());
            }
        }
    }
}