using model.timing;
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

        async void IEffect.Resolve(Game game) // TODO make IEffect.Resolve async?
        {
            UnityEngine.Debug.Log("Initiating run on " + server);
            var run = new RunStructure(server, game);
            await run.AwaitEnd();
        }

        void IEffect.Observe(IImpactObserver observer, Game game)
        {
            observer.NotifyImpact(true, this);
        }
    }
}