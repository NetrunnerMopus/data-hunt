using UnityEngine;

namespace effects.runner
{
    public class SelfTrash : IEffect
    {
        private ICard2 card;

        public SelfTrash(ICard2 card)
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