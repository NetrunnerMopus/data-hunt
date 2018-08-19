using model.zones.corp;

namespace model.effects.runner
{
    public class Run : IEffect
    {
        private readonly IServer server;

        public Run(IServer server)
        {
            this.server = server;
        }

        void IEffect.Resolve(Game game)
        {
            UnityEngine.Debug.Log("Initiating run on " + server);
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}