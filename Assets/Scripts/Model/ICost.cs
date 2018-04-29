using model.play;

namespace model
{
    public interface ICost
    {
        bool CanPay(Game game);
        void Pay(Game game);
        void Observe(IAvailabilityObserver<ICost> observer, Game game);
    }
}