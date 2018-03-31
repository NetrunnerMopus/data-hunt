using UnityEngine;

namespace model.effects.runner
{
    public class SelfTrash : IEffect
    {
        private ICard card;

        public SelfTrash(ICard card)
        {
            this.card = card;
        }

        void IEffect.Resolve(Game game, MonoBehaviour source)
        {
            source.transform.SetParent(game.runner.heap.Zone.transform);
            Object.Destroy(source);
        }
    }
}