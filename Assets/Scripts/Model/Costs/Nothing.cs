using System.Threading.Tasks;

namespace model.costs
{
    public class Nothing : ICost
    {
        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observer.NotifyPayable(true, this);
        }

        async Task ICost.Pay(Game game)
        {
            await Task.CompletedTask;
        }

        bool ICost.Payable(Game game) => true;
    }
}