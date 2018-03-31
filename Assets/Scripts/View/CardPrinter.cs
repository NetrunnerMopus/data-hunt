using UnityEngine;
using UnityEngine.UI;

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

        public GameObject Print(string name, string asset)
        {
            var gameObject = new GameObject(name)
            {
                layer = 5
            };

            var image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>(asset);
            image.preserveAspect = true;
            var canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = true;
            gameObject.transform.SetParent(transform);
            return gameObject;
        }
    }
}