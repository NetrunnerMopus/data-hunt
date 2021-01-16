using model.player;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.play
{
    public class SimultaneousTriggers
    {
        public IList<IEffect> untriggered;

        public SimultaneousTriggers(IList<IEffect> effects)
        {
            untriggered = effects;
        }

        async public Task AllTriggered(IPilot pilot, Game game)
        {
            while (untriggered.Count > 0)
            {
                UnityEngine.Debug.Log("Picking the next effect to fire among " + untriggered);
                var effect = await pilot.TriggerFromSimultaneous(untriggered);
                await effect.Resolve(game);
                untriggered.Remove(effect);
            }
        }
    }
}
