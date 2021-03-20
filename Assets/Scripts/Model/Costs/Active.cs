using System;
using System.Threading.Tasks;
using model.cards;

namespace model.costs
{
    public class Active : ICost
    {
        private Card card;
        bool ICost.Payable => card.Active;
        public event Action<ICost, bool> ChangedPayability = delegate { };

        public Active(Card card)
        {
            this.card = card;
            card.Toggled += delegate
            {
                ChangedPayability(this, card.Active);
            };
        }

        async Task ICost.Pay() => await Task.CompletedTask;
    }
}
