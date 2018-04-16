using UnityEngine;
using controller;
using UnityEngine.UI;

namespace view.gui
{
    public class StackPile : MonoBehaviour, IStackView
    {
        private GameObject top;

        void Start()
        {
            gameObject.AddComponent<CardPrinter>();
        }

        void IStackView.UpdateCardsLeft(int cardsLeft)
        {
            Destroy(top);
            var text = GetComponentInChildren<Text>();
            text.text = cardsLeft + " cards";
            if (cardsLeft > 0)
            {
                top = GetComponent<CardPrinter>().PrintRunnerFacedown("Top of stack");
                top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
                var rect = top.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
                top.AddComponent<TopOfTheStack>();
            }
        }
    }
}