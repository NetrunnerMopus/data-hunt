using UnityEngine;
using controller;
using UnityEngine.UI;
using model;
using model.zones.runner;

namespace view.gui
{
    public class StackPile : MonoBehaviour, IStackPopObserver, IStackCountObserver
    {
        public Game Game { private get; set; }
        private GameObject top;
        private DropZone grip;

        void Awake()
        {
            gameObject.AddComponent<CardPrinter>();
            grip = GameObject.Find("Grip").AddComponent<DropZone>();
        }

        void IStackPopObserver.NotifyCardPopped(bool empty)
        {
            Destroy(top);
            if (!empty)
            {
                top = GetComponent<CardPrinter>().PrintRunnerFacedown("Top of stack");
                top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
                var rect = top.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
                top
                    .AddComponent<Droppable>()
                    .Represent(
                        Game.runner.actionCard.draw,
                        Game,
                        grip
                    );
            }
        }

        void IStackCountObserver.NotifyCardCount(int cards)
        {
            var text = GetComponentInChildren<Text>();
            text.text = cards + " cards";
        }
    }
}