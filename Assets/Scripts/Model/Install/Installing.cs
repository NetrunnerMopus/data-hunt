using model.cards;
using model.player;
using model.zones;

namespace model.install
{
    public partial class Installing
    {
        private IPilot pilot;
        private Zone playArea;

        public Installing(IPilot pilot, Zone playArea)
        {
            this.pilot = pilot;
            this.playArea = playArea;
        }

        public IEffect InstallingCard(Card card)
        {
            return new GenericInstall(card, pilot, playArea);
        }
    }
}
