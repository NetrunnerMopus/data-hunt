namespace model
{
    public interface ICost
    {
        bool CanPay(Game game);
        void Pay(Game game);
        void Observe(IPayabilityObserver observer, Game game);
    }

    public interface IPayabilityObserver
    {
        void NotifyPayable(bool payable, ICost source);
    }
}