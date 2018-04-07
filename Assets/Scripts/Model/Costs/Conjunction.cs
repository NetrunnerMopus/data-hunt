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

        void ICost.Pay(Game game)
        {
            foreach (var cost in costs)
            {
                cost.Pay(game);
            }
        }
    }
}