using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model.play;

namespace model.abilities {
    public class PadCampaignDrip : IAbility {
        public ISource Source => throw new System.NotImplementedException();

        public bool Active => throw new System.NotImplementedException();

        public Task Resolve() {
            throw new System.NotImplementedException();
        }

        public Condition TriggerCondition() {
            return YourTurnBegins();
        }

        public PadCampaignDripInstance CreatePending() {
            NextReactionWindow().Add(new PadCampaignDripInstance())
        }
    }
}
