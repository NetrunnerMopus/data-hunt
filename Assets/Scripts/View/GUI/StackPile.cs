using controller;
using model;
using model.zones;
using UnityEngine;

namespace view.gui
{
    public class StackPile
    {
        private DropZone gripZone;
        private GameObject top;

        public StackPile(GameObject gameObject, Runner runner, DropZone gripZone, BoardParts parts)
        {
            this.gripZone = gripZone;
            top = parts.Print(gameObject).PrintRunnerFacedown("Top of stack");
            top.transform.SetAsFirstSibling();
            top.transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 90.0f);
            var rect = top.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            top
                .AddComponent<Droppable>()
                .Represent(
                    new InteractiveAbility(runner.Acting.draw, gripZone)
                );
            var pileCount = gameObject.AddComponent<PileCount>();
            runner.zones.stack.zone.Changed += pileCount.UpdateCardCount;
            runner.zones.stack.zone.Changed += UpdateTopOfStack;
        }

        private void UpdateTopOfStack(Zone zone)
        {
            if (top != null)
            {
                top.SetActive(zone.Count > 0);
            }
        }
    }
}
