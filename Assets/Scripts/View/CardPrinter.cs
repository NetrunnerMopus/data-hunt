using controller;
using model;
using model.cards;
using model.player;
using UnityEngine;
using UnityEngine.UI;
using view.gui;

namespace view
{
    public class CardPrinter
    {
        private RawCardPrinter raw;
        private IPerception perception;
        private CardZoom zoom;
        public bool Sideways = false;

        public CardPrinter(GameObject parent, IPerception perception, CardZoom zoom)
        {
            this.raw = new RawCardPrinter(parent);
            this.perception = perception;
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

        public GameObject Print(Card card)
        {
            var gameObject = Print(card.Name, FaceSprites.ChooseFace(card, perception));
            gameObject.AddComponent<PrintedCard>().Represent(card, perception, Sideways);
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

        private class PrintedCard : MonoBehaviour
        {
            private Card card;
            private IPerception perception;
            private bool archives;

            public PrintedCard Represent(Card card, IPerception perception, bool archives)
            {
                this.card = card;
                this.perception = perception;
                this.archives = archives;
                card.ChangedInfo += UpdateFace;
                Rotate();
                return this;
            }

            private void UpdateFace(Card card, Information information)
            {
                var image = gameObject.GetComponent<Image>();
                image.sprite = FaceSprites.ChooseFace(card, perception);
                Rotate();
            }

            private void Rotate()
            {
                if (archives)
                {
                    var image = gameObject.GetComponent<Image>();
                    if (card.Information == Information.HIDDEN_FROM_RUNNER)
                    {
                        image.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                    }
                    else
                    {
                        image.rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 00.0f);
                    }
                }
            }

            void OnDestroy()
            {
                card.ChangedInfo -= UpdateFace;
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

        public static Sprite ChooseFace(Card card, IPerception perception)
        {
            if (perception.CanSee(card.Information))
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
