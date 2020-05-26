using UnityEngine;
using UnityEngine.UI;
using model.cards;
using model;
using controller;
using view.gui;

namespace view
{
    public class CardPrinter
    {
        private RawCardPrinter raw;
        private CardZoom zoom;
        public bool Sideways = false;

        public CardPrinter(GameObject parent, CardZoom zoom)
        {
            this.raw = new RawCardPrinter(parent);
            this.zoom = zoom;
        }

        public GameObject PrintCorpFacedown(string name)
        {
            return Print(name, FaceSprites.CORP_BACK);
        }

        public GameObject PrintRunnerFacedown(string name)
        {
            return Print(name, FaceSprites.RUNNER_BACK);
        }

        public GameObject PrintFlippable(Card card)
        {
            var gameObject = Print(card.Name, FaceSprites.ChooseFace(card));
            gameObject.AddComponent<PrintedCard>().Represent(card, Sideways);
            gameObject.AddComponent<CardInspection>().Represent(zoom, card);
            return gameObject;
        }

        public GameObject Print(Card card)
        {
            var gameObject = Print(card.Name, "Images/Cards/" + card.FaceupArt);
            gameObject.AddComponent<CardInspection>().Represent(zoom, card);
            return gameObject;
        }

        public GameObject Print(string name, string asset)
        {
            return Print(name, Resources.Load<Sprite>(asset));
        }

        private GameObject Print(string name, Sprite sprite)
        {
            return raw.Print(name, sprite);
        }

        private class PrintedCard : MonoBehaviour, IFlipObserver
        {
            private Card card;
            private bool archives;

            public PrintedCard Represent(Card card, bool archives)
            {
                this.card = card;
                this.archives = archives;
                card.ObserveFlips(this);
                Rotate();
                return this;
            }

            void IFlipObserver.NotifyFlipped(bool faceup)
            {
                var image = GetComponent<Image>();
                image.sprite = FaceSprites.ChooseFace(card);
                Rotate();
            }

            private void Rotate()
            {
                var image = GetComponent<Image>();
                if (archives)
                {
                    if (card.Faceup)
                    {
                        image.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        image.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                    }
                }
            }

            void OnDestroy()
            {
                card.UnobserveFlips(this);
            }
        }
    }

    public class RawCardPrinter
    {
        private GameObject parent;

        public RawCardPrinter(GameObject parent)
        {
            this.parent = parent;
        }

        public GameObject Print(string name, Sprite sprite)
        {
            var card = new GameObject(name)
            {
                layer = 5
            };
            var image = card.AddComponent<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;
            card.transform.SetParent(parent.transform);
            var rectangle = image.rectTransform;
            rectangle.anchorMin = new Vector2(0.1f, 0.1f);
            rectangle.anchorMax = new Vector2(0.9f, 0.9f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return card;
        }
    }


    class FaceSprites
    {
        public static Sprite CORP_BACK = Resources.Load<Sprite>("Images/UI/corp-card-back");
        public static Sprite RUNNER_BACK = Resources.Load<Sprite>("Images/UI/runner-card-back");

        public static Sprite ChooseFace(Card card)
        {
            if (card.Faceup)
            {
                return Resources.Load<Sprite>("Images/Cards/" + card.FaceupArt);
            }
            else if (card.Faction.Side == Side.CORP)
            {
                return CORP_BACK;
            }
            else
            {
                return RUNNER_BACK;
            }
        }
    }
}
