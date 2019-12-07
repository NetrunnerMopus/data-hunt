namespace model
{
    public interface ICost
    {
        void Pay(Game game);
        void Observe(IPayabilityObserver observer, Game game);
        bool Payable(Game game);
    }

    public interface IPayabilityObserver
    {
        void NotifyPayable(bool payable, ICost source);
    }
}