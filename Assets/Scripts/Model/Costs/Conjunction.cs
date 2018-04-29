using model.play;

namespace model.costs
{
    public class Conjunction : ICost
    {
        private ICost[] costs;

        public Conjunction(params ICost[] costs)
        {
            this.costs = costs;
        }

        bool ICost.CanPay(Game game)
        {
            foreach (var cost in costs)
            {
                if (!cost.CanPay(game))
                {
                    return false;
                }
            }
            return true;
        }

        void ICost.Observe(IAvailabilityObserver<ICost> observer, Game game)
        {
            throw new System.NotImplementedException();
        }

        void ICost.Pay(Game game)
        {
            foreach (var cost in costs)
            {
                cost.Pay(game);
            }
        }
    }
}