using System.Threading.Tasks;
using model.cards;

namespace model.play
{
    public class Rezzable
    {
        public readonly Card card;
        private readonly Game game;
        public bool CanRez {get; private set;}
        public event AsyncAction<Rezzable> Changed;

        public Rezzable(Card card, Game game)
        {
            this.card = card;
            this.game = game;
            card.PlayCost.ChangedPayability += UpdateRezzability;
        }

        async public Task Rez()
        {
            UnityEngine.Debug.Log("Rezzing " + card.Name);
            card.FlipFaceUp();
            await card.PlayCost.Pay();
            await card.Activate();
            UnityEngine.Debug.Log("Rezzed " + card.Name);
        }

        async private void UpdateRezzability(ICost rezCost, bool payable)
        {
            CanRez = payable;
            await Changed?.Invoke(this);
        }
    }
}
