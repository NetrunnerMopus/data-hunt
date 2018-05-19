namespace model.costs
{
    public class Nothing : ICost
    {
        void ICost.Observe(IPayabilityObserver observer, Game game)
        {
            observer.NotifyPayable(true, this);
        }

        void ICost.Pay(Game game)
        {
        }
    }
}