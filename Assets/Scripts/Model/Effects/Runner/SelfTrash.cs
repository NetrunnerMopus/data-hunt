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

        void IEffect.Resolve(Game game)
        {
            game.runner.heap.Add(card);
        }
    }
}