using UnityEngine;
using controller;
using UnityEngine.UI;

namespace view
{
    public class StackPile : MonoBehaviour
    {
        public GripZone grip;
        private GameObject top;

        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        public void UpdateCardsLeft(int cardsLeft)
        {
            Object.Destroy(top);
            var text = GetComponentInChildren<Text>();
            text.text = cardsLeft + " cards";
            if (cardsLeft > 0)
            {
                top = GetComponent<CardPrinter>().PrintRunnerFacedown("Top of stack");
                top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
                var rect = top.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
                var topOfTheStack = top.AddComponent<TopOfTheStack>();
                topOfTheStack.gripZone = grip;
            }
        }
    }
}