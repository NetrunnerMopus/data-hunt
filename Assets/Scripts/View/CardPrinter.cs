using UnityEngine;
using UnityEngine.UI;
using model.cards;

namespace view
{
    public class CardPrinter : MonoBehaviour
    {
        public GameObject PrintCorpFacedown(string name)
        {
            return Print(name, "Images/UI/corp-card-back");
        }

        public GameObject PrintRunnerFacedown(string name)
        {
            return Print(name, "Images/UI/runner-card-back");
        }

        public GameObject Print(Card card)
        {
            return Print(card.Name, "Images/Cards/" + card.FaceupArt);
        }

        public GameObject Print(string name, string asset)
        {
            var gameObject = new GameObject(name)
            {
                layer = 5
            };
            var image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>(asset);
            image.preserveAspect = true;
            gameObject.transform.SetParent(transform);
            var rectangle = image.rectTransform;
            rectangle.anchorMin = new Vector2(0.1f, 0.1f);
            rectangle.anchorMax = new Vector2(0.9f, 0.9f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return gameObject;
        }
    }
}