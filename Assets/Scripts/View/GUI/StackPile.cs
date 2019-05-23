using UnityEngine;
using controller;
using model;
using model.zones.runner;
using model.zones;

namespace view.gui
{
    public class StackPile : MonoBehaviour, IZoneCountObserver
    {
        private Game game;
        private DropZone gripZone;
        private GameObject top;
        public void Construct(Game game, DropZone gripZone)
        {
            this.game = game;
            this.gripZone = gripZone;
        }

        void Start()
        {
            var printer = gameObject.AddComponent<CardPrinter>();
            top = printer.PrintRunnerFacedown("Top of stack");
            top.transform.SetAsFirstSibling();
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

        void IZoneCountObserver.NotifyCount(int count)
        {
            if (top != null)
            {
                top.SetActive(count > 0);
            }
        }
    }
}