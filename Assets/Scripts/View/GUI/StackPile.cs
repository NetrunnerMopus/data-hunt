using UnityEngine;
using controller;
using UnityEngine.UI;
using model;
using model.zones.runner;

namespace view.gui
{
    public class StackPile : IStackPopObserver, IStackCountObserver
    {
        private GameObject gameObject;
        private Game game;
        private DropZone gripZone;
        private GameObject top;
        private CardPrinter printer;

        public StackPile(GameObject gameObject, Game game, DropZone gripZone)
        {
            printer = gameObject.AddComponent<CardPrinter>();
            this.gameObject = gameObject;
            this.game = game;
            this.gripZone = gripZone;
        }

        void IStackPopObserver.NotifyCardPopped(bool empty)
        {
            Object.Destroy(top);
            if (!empty)
            {
                top = printer.PrintRunnerFacedown("Top of stack");
                top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
                var rect = top.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
                top
                    .AddComponent<DroppableAbility>()
                    .Represent(
                        game.runner.actionCard.draw,
                        game,
                        gripZone
                    );
            }
        }

        void IStackCountObserver.NotifyCardCount(int cards)
        {
            var text = gameObject.GetComponentInChildren<Text>();
            text.text = cards + " cards";
        }
    }
}