using model.cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.play.corp
{
    public class Rezzable : IPayabilityObserver
    {
        public readonly Card card;
        private readonly Game game;
        private HashSet<IRezzableObserver> observers = new HashSet<IRezzableObserver>();

        public Rezzable(Card card, Game game)
        {
            this.card = card;
            this.game = game;
        }

        public void Rez()
        {
            UnityEngine.Debug.Log("Rezzing " + card.Name);
            card.FlipFaceUp();
            card.PlayCost.Pay(game);
            card.Activation.Resolve(game);
            UnityEngine.Debug.Log("Rezzed " + card.Name);
        }

        public void ObserveRezzable(IRezzableObserver observer)
        {
            observers.Add(observer);
            card.PlayCost.Observe(this, game);
        }

        void IPayabilityObserver.NotifyPayable(bool payable, ICost source)
        {
            if (payable && !card.Faceup)
            {
                foreach (var observer in observers)
                {
                    observer.NotifyRezzable(this);
                }
            }
            else
            {
                foreach (var observer in observers)
                {
                    observer.NotifyNotRezzable();
                }
            }
        }
    }

    public interface IRezzableObserver
    {
        Task NotifyRezzable(Rezzable rezzable);
        Task NotifyNotRezzable();
    }
}