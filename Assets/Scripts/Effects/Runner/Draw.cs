using UnityEngine;

namespace effects.runner
{
    public class Draw : IEffect
    {
        private int cards;

        public Draw(int cards)
        {
            this.cards = cards;
        }

        void IEffect.Resolve(Game game, MonoBehaviour source)
        {
            for (int i = 0; i < cards; i++)
            {
                game.runner.stack.Draw();
            }
        }
    }
}