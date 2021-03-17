using System;
using System.Threading.Tasks;
using model.cards;

namespace model.costs
{
    public class Active : ICost
    {
        private Card card;
        public event Action<ICost, bool> PayabilityChanged = delegate { };

        public Active(Card card)
        {
            this.card = card;
            card.Toggled += delegate
            {
                PayabilityChanged(this, card.Active);
            };
        }

        bool ICost.Payable(Game game) => card.Active;

        async Task ICost.Pay(Game game)
        {
            await Task.CompletedTask;
        }
    }
}
