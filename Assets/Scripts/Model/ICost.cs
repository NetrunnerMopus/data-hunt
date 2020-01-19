using System.Threading.Tasks;

namespace model
{
    public interface ICost
    {
        Task Pay(Game game);
        void Observe(IPayabilityObserver observer, Game game);
        bool Payable(Game game);
    }

    public interface IPayabilityObserver
    {
        void NotifyPayable(bool payable, ICost source);
    }
}