using UnityEngine;

namespace model.effects.runner
{
    public class TagRemoval : IEffect
    {
        private Runner runner;
        private int tags;

        public TagRemoval(int tags, Runner runner)
        {
            this.tags = tags;
            this.runner = runner;
        }

        void IEffect.Resolve(Game game, MonoBehaviour source)
        {
            runner.tags -= tags;
        }
    }
}