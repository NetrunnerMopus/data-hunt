using UnityEngine;
using controller;
using model;
using model.zones;

namespace view.gui
{
    public class StackPile : IZoneCountObserver
    {
        private Game game;
        private DropZone gripZone;
        private GameObject top;

        public StackPile(GameObject gameObject, Game game, DropZone gripZone, BoardParts parts)
        {
            this.game = game;
            this.gripZone = gripZone;
            top = parts.Print(gameObject).PrintRunnerFacedown("Top of stack");
            top.transform.SetAsFirstSibling();
            top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
            var rect = top.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            top
                .AddComponent<Droppable>()
                .Represent(
                    new InteractiveAbility(game.runner.actionCard.draw, game),
                    gripZone
                );
            game.runner.zones.stack.zone.ObserveCount(gameObject.AddComponent<PileCount>());
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
