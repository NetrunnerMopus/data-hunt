using model.cards;
using UnityEngine;
using UnityEngine.UI;

namespace view.gui
{
    public class CardInServer : MonoBehaviour, IFlipObserver
    {
        private Card card;
        private Image image;

        public void Represent(Card card, Image image)
        {
            this.card = card;
            this.image = image;
            card.ObserveFlips(this);
        }

        void IFlipObserver.NotifyFlipped(bool faceup)
        {
            image.sprite = Resources.Load<Sprite>("Images/Cards/" + card.FaceupArt);
        }
    }
}